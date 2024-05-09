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
              @change="selectedIssues = ($event.target as HTMLInputElement)?.checked ? issues.map((p) => p.id) : []" />
          </th>
          <th scope="col" class="px-3 py-3.5 text-left text-sm font-semibold text-white"></th>
          <th scope="col" class="px-3 py-3.5 text-left text-sm font-semibold text-white">First Seen</th>
          <th scope="col" class="px-3 py-3.5 text-left text-sm font-semibold text-white">Last Seen</th>
        </tr>
      </thead>
      <tbody v-if="issues.length > 0" class="divide-y divide-white/5">
        <tr v-for="issue in issues" :key="issue.id" :class="[selectedIssues.includes(issue.id) && 'bg-gray-800']">
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
              <div class="truncate text-sm font-medium text-white"><RouterLink :to="'/issue/' + issue.id">{{ issue.title }}</RouterLink></div>
            </div>
          </td>
          <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">
            {{ new Date(issue.createdAt).toLocaleString() }}
          </td>
          <td class="whitespace-nowrap px-3 py-4 text-sm text-gray-500">
            {{ new Date(issue.lastSeen).toLocaleString() }}
          </td>
        </tr>
      </tbody>
    </table>
    <div v-if="issues.length === 0">
      <div class="rounded-md bg-green-900 p-4 m-4">
        <div class="flex">
          <div class="flex-shrink-0">
            <CheckCircleIcon class="h-5 w-5 text-green-400" aria-hidden="true" />
          </div>
          <div class="ml-3">
            <h3 class="text-sm font-medium text-green-50">Hurrah! No issues!</h3>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useRoute } from 'vue-router';
import { ref, computed } from 'vue';
import { CheckCircleIcon } from '@heroicons/vue/20/solid'

const route = useRoute();

interface Issue {
  id: string;
  title: string;
  href: string;
  level: string;
  createdAt: Date;
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
  createdAt: Date;
  lastSeen: Date;
}

connection.on("issueReceived", (issueReceived: IssueDto) => {
  console.info("issueReceived", issueReceived.title);
  const existingIssueIndex = issues.value.findIndex(issue => issue.id === issueReceived.id);

  if (existingIssueIndex !== -1) {
    // Replace existing issue
    console.info("replacing issue, last seen" + issueReceived.lastSeen);
    issues.value.splice(existingIssueIndex, 1, {
      id: issueReceived.id,
      href: '/issue/' + issueReceived.id,
      title: issueReceived.title,
      createdAt: issueReceived.createdAt,
      lastSeen: issueReceived.lastSeen,
      level: 'error'
    });
  } else {
    // Add new issue
    issues.value.push({
      id: issueReceived.id,
      href: '/issue/' + issueReceived.id,
      title: issueReceived.title,
      createdAt: issueReceived.createdAt,
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