<template>
  <header class="flex items-center justify-between border-b border-white/5 px-4 py-4 sm:px-6 sm:py-6 lg:px-8">
    <h1 class="text-base font-semibold leading-7 text-white">Projects</h1>

    <!-- Sort dropdown -->
    <Menu as="div" class="relative">
      <MenuButton class="flex items-center gap-x-1 text-sm font-medium leading-6 text-white">
        Sort by
        <ChevronUpDownIcon class="h-5 w-5 text-gray-500" aria-hidden="true" />
      </MenuButton>
      <transition enter-active-class="transition ease-out duration-100" enter-from-class="transform opacity-0 scale-95"
        enter-to-class="transform opacity-100 scale-100" leave-active-class="transition ease-in duration-75"
        leave-from-class="transform opacity-100 scale-100" leave-to-class="transform opacity-0 scale-95">
        <MenuItems
          class="absolute right-0 z-10 mt-2.5 w-40 origin-top-right rounded-md bg-white py-2 shadow-lg ring-1 ring-gray-900/5 focus:outline-none">
          <MenuItem v-slot="{ active }">
          <a href="#" :class="[active ? 'bg-gray-50' : '', 'block px-3 py-1 text-sm leading-6 text-gray-900']">Name</a>
          </MenuItem>
          <MenuItem v-slot="{ active }">
          <a href="#" :class="[active ? 'bg-gray-50' : '', 'block px-3 py-1 text-sm leading-6 text-gray-900']">Date
            updated</a>
          </MenuItem>
          <MenuItem v-slot="{ active }">
          <a href="#"
            :class="[active ? 'bg-gray-50' : '', 'block px-3 py-1 text-sm leading-6 text-gray-900']">Environment</a>
          </MenuItem>
        </MenuItems>
      </transition>
    </Menu>
  </header>

  <!-- Issue list -->
  <ul role="list" class="divide-y divide-white/5">
    <li v-for="project in projects" :key="project.id"
      class="relative flex items-center space-x-4 px-4 py-4 sm:px-6 lg:px-8">
      <div class="min-w-0 flex-auto">
        <div class="flex items-center gap-x-3">
          <!-- <div :class="[statuses[project.status], 'flex-none rounded-full p-1']">
            <div class="h-2 w-2 rounded-full bg-current" />
          </div> -->
          <h2 class="min-w-0 text-sm font-semibold leading-6 text-white">
            <a :href="project.href" class="flex gap-x-2">
              <!-- <span class="truncate">{{ project.teamName }}</span> -->
              <span class="text-gray-400">/</span>
              <span class="whitespace-nowrap">{{ project.projectName }}</span>
              <span class="absolute inset-0" />
            </a>
          </h2>
        </div>
        <!-- <div class="mt-3 flex items-center gap-x-2.5 text-xs leading-5 text-gray-400">
          <p class="truncate">{{ project.description }}</p>
          <svg viewBox="0 0 2 2" class="h-0.5 w-0.5 flex-none fill-gray-300">
            <circle cx="1" cy="1" r="1" />
          </svg>
          <p class="whitespace-nowrap">{{ project.statusText }}</p>
        </div> -->
      </div>
      <!-- <div
        :class="[environments[deployment.environment], 'rounded-full flex-none py-1 px-2 text-xs font-medium ring-1 ring-inset']">
        {{ deployment.environment }}</div>
      <ChevronRightIcon class="h-5 w-5 flex-none text-gray-400" aria-hidden="true" /> -->
    </li>
  </ul>
</template>

<style scoped>
header {
  line-height: 1.5;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }
}
</style>

<script setup lang="ts">
import { ref } from 'vue'
import {
  Menu,
  MenuButton,
  MenuItem,
  MenuItems,
} from '@headlessui/vue'
import { ChevronUpDownIcon } from '@heroicons/vue/20/solid'


interface AddProjectMessage {
  id: string;
  projectName: string;
}

interface Project {
  id: string;
  href: string;
  projectName: string;
}

const projects = ref<Project[]>([]);

import { HubConnectionBuilder } from '@microsoft/signalr';

const connection = new HubConnectionBuilder()
    .withUrl("/crashstack")
    .build();

connection.on("projectReceived", (addProjectMessage: AddProjectMessage) => {
  console.info("projectReceived", addProjectMessage.projectName);
  projects.value.push({
    id: addProjectMessage.id,
    href: '/project/' + addProjectMessage.id,
    projectName: addProjectMessage.projectName
  });
});

connection.start().then(() => console.info("connection open")).catch((err) => document.write(err));
</script>