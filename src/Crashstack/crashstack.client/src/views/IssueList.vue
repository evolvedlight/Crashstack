<template>
  <div class="bg-gray-900 py-10">
    <h2 class="px-4 text-base font-semibold leading-7 text-white sm:px-6 lg:px-8">Issues</h2>
    <table class="min-w-full table-fixed divide-y divide-gray-300">
      <thead>
        <tr>
          <th scope="col" class="relative px-7 sm:w-12 sm:px-6">
            <input type="checkbox"
              class="absolute left-4 top-1/2 -mt-2 h-4 w-4 rounded border-gray-300 text-indigo-600 focus:ring-indigo-600"
              :checked="indeterminate || selectedIssues.length === issues.length" :indeterminate="indeterminate"
              @change="selectedIssues = $event.target.checked ? issues.map((p) => p.id) : []" />
          </th>
          <th scope="col" class="min-w-[12rem] py-3.5 pr-3 text-left text-sm font-semibold text-gray-900">Name</th>
          <th scope="col" class="px-3 py-3.5 text-left text-sm font-semibold text-white-900"></th>
          <th scope="col" class="px-3 py-3.5 text-left text-sm font-semibold text-gray-900">Email</th>
          <th scope="col" class="px-3 py-3.5 text-left text-sm font-semibold text-gray-900">Role</th>
        </tr>
      </thead>
      <tbody class="divide-y divide-white/5">
        <tr v-for="issue in issues" :key="issue.id"
          :class="[selectedIssues.includes(issue.id) && 'bg-gray-800']">
          <td class="relative px-7 sm:w-12 sm:px-6">
            <div v-if="selectedIssues.includes(issue.id)" class="absolute inset-y-0 left-0 w-0.5 bg-indigo-600">
            </div>
            <input type="checkbox"
              class="absolute left-4 top-1/2 -mt-2 h-4 w-4 rounded border-gray-300 text-indigo-600 focus:ring-indigo-600"
              :value="issue.id" v-model="selectedIssues" />
          </td>
          <td
            :class="['whitespace-nowrap py-4 pr-3 text-sm font-medium', selectedIssues.includes(issue.id) ? 'text-indigo-600' : 'text-gray-900']">
            <div class="flex flex-col gap-x-4">
              <div class="truncate text-sm font-medium text-white">{{ issue.title }}</div>
              <div class="truncate text-sm font-small text-white">{{ issue.href }}</div>
              <div class="truncate text-sm font-small text-white">{{ issue.id }}</div>
            </div>
          </td>
          <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">
            {{ issue.title }}
          </td>
          <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">
            {{ issue.id }}
          </td>
          <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">
            {{ issue.lastSeen }}
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup lang="ts">
import { useRoute } from 'vue-router';
import { ref, computed } from 'vue';


const route = useRoute();

import {
  Menu,
  MenuButton,
  MenuItem,
  MenuItems,
} from '@headlessui/vue'
import { ChevronRightIcon, ChevronUpDownIcon } from '@heroicons/vue/20/solid'

const statuses: Record<string, string> = {
  offline: 'text-gray-500 bg-gray-100/10',
  online: 'text-green-400 bg-green-400/10',
  error: 'text-rose-400 bg-rose-400/10',
}

const environments: Record<string, string> = {
  Preview: 'text-gray-400 bg-gray-400/10 ring-gray-400/20',
  Production: 'text-indigo-400 bg-indigo-400/10 ring-indigo-400/30',
}

interface Issue {
  id: string;
  title: string;
  href: string;
  level: string;
  lastSeen: Date;
}

const issues = ref<Issue[]>([]);
const selectedIssues = ref<String[]>([]);
const indeterminate = computed(() => selectedIssues.value.length > 0 && selectedIssues.value.length < issues.value.length)

import { HubConnectionBuilder } from '@microsoft/signalr';

const connection = new HubConnectionBuilder()
  .withUrl("/crashstack")
  .build();

interface IssueDto {
  id: string;
  title: string;
  href: string;
  lastSeen: Date;
}

connection.on("issueReceived", (issueReceived: IssueDto) => {
  console.info("issueReceived", issueReceived.title);
  const existingIssueIndex = issues.value.findIndex(issue => issue.id === issueReceived.id);

  if (existingIssueIndex !== -1) {
    // Replace existing issue
    issues.value.splice(existingIssueIndex, 1, {
      id: issueReceived.id,
      href: '/issue/' + issueReceived.id,
      title: issueReceived.title,
      lastSeen: issueReceived.lastSeen,
      level: 'error'
    });
  } else {
    // Add new issue
    issues.value.push({
      id: issueReceived.id,
      href: '/issue/' + issueReceived.id,
      title: issueReceived.title,
      lastSeen: issueReceived.lastSeen,
      level: 'error'
    });
  }
});

connection.start().then(() => {
  console.info("connection open");
  connection.send("SubscribeToProject", route.params.id);
}).catch((err) => document.write(err));
</script>