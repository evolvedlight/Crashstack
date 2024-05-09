<template>
   <div class="py-4">
      <div class="px-4 sm:px-6 lg:px-8">
        <div class="xl:col-span-2 xl:border-r xl:border-gray-200 xl:pr-8">
          <div>
            <div>
              <div class="md:flex md:items-center md:justify-between md:space-x-4 xl:border-b xl:pb-6">
                <div>
                  <h1 class="text-2xl font-bold text-gray-100">{{ issue.title }}</h1>
                  <p class="mt-2 text-sm text-gray-500">
                    #{{ issue.id }} opened by
                    {{ ' ' }}
                    <a href="#" class="font-medium text-gray-900">Hilary Mahy</a>
                    {{ ' ' }}
                    in
                    {{ ' ' }}
                    <a href="#" class="font-medium text-gray-900">Customer Portal</a>
                  </p>
                </div>
                <div class="mt-4 flex space-x-3 md:mt-0">
                  <button type="button" class="inline-flex justify-center gap-x-1.5 rounded-md bg-white px-3 py-2 text-sm font-semibold text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 hover:bg-gray-50">
                    <PencilIcon class="-ml-0.5 h-5 w-5 text-gray-400" aria-hidden="true" />
                    Edit
                  </button>
                  <button type="button" class="inline-flex justify-center gap-x-1.5 rounded-md bg-white px-3 py-2 text-sm font-semibold text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 hover:bg-gray-50">
                    <BellIcon class="-ml-0.5 h-5 w-5 text-gray-400" aria-hidden="true" />
                    Subscribe
                  </button>
                </div>
              </div>
              <aside class="mt-8 xl:hidden">
                <h2 class="sr-only">Details</h2>
                <div class="space-y-5">
                  <div class="flex items-center space-x-2">
                    <LockOpenIcon class="h-5 w-5 text-green-500" aria-hidden="true" />
                    <span class="text-sm font-medium text-green-700">Open Issue</span>
                  </div>
                  <div class="flex items-center space-x-2">
                    <ChatBubbleLeftEllipsisIcon class="h-5 w-5 text-gray-400" aria-hidden="true" />
                    <span class="text-sm font-medium text-gray-900">4 comments</span>
                  </div>
                  <div class="flex items-center space-x-2">
                    <CalendarIcon class="h-5 w-5 text-gray-400" aria-hidden="true" />
                    <span class="text-sm font-medium text-gray-900">Created on <time datetime="2020-12-02">Dec 2, 2020</time></span>
                  </div>
                </div>
                <div class="mt-6 space-y-8 border-b border-t border-gray-200 py-6">
                  <div>
                    <h2 class="text-sm font-medium text-gray-500">Assignees</h2>
                    <ul role="list" class="mt-3 space-y-3">
                      <li class="flex justify-start">
                        <a href="#" class="flex items-center space-x-3">
                          <div class="flex-shrink-0">
                            <img class="h-5 w-5 rounded-full" src="https://images.unsplash.com/photo-1520785643438-5bf77931f493?ixlib=rb-=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=facearea&facepad=8&w=256&h=256&q=80" alt="" />
                          </div>
                          <div class="text-sm font-medium text-gray-900">Eduardo Benz</div>
                        </a>
                      </li>
                    </ul>
                  </div>
                  <div>
                    <h2 class="text-sm font-medium text-gray-500">Tags</h2>
                    <ul role="list" class="mt-2 leading-8">
                      <li class="inline">
                        <a href="#" class="relative inline-flex items-center rounded-full px-2.5 py-1 ring-1 ring-inset ring-gray-300 hover:bg-gray-50">
                          <div class="absolute flex flex-shrink-0 items-center justify-center">
                            <span class="h-1.5 w-1.5 rounded-full bg-rose-500" aria-hidden="true" />
                          </div>
                          <div class="ml-3 text-xs font-semibold text-gray-900">Bug</div>
                        </a>
                        {{ ' ' }}
                      </li>
                      <li class="inline">
                        <a href="#" class="relative inline-flex items-center rounded-full px-2.5 py-1 ring-1 ring-inset ring-gray-300 hover:bg-gray-50">
                          <div class="absolute flex flex-shrink-0 items-center justify-center">
                            <span class="h-1.5 w-1.5 rounded-full bg-indigo-500" aria-hidden="true" />
                          </div>
                          <div class="ml-3 text-xs font-semibold text-gray-900">Accessibility</div>
                        </a>
                        {{ ' ' }}
                      </li>
                    </ul>
                  </div>
                </div>
              </aside>
              <div class="py-3 xl:pb-0 xl:pt-6">
                <h2 class="sr-only">Description</h2>
                <div class="prose max-w-none">
                  <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Expedita, hic? Commodi cumque similique id tempora molestiae deserunt at suscipit, dolor voluptatem, numquam, harum consequatur laboriosam voluptas tempore aut voluptatum alias?</p>
                  <ul role="list">
                    <li>Tempor ultrices proin nunc fames nunc ut auctor vitae sed. Eget massa parturient vulputate fermentum id facilisis nam pharetra. Aliquet leo tellus.</li>
                    <li>Turpis ac nunc adipiscing adipiscing metus tincidunt senectus tellus.</li>
                    <li>Semper interdum porta sit tincidunt. Dui suspendisse scelerisque amet metus eget sed. Ut tellus in sed dignissim.</li>
                  </ul>
                </div>
              </div>
            </div>
          </div>
          <section aria-labelledby="activity-title" class="mt-8 xl:mt-10">
            <div>
              <div class="divide-y divide-gray-200">
                <div class="pb-4">
                  <h2 id="activity-title" class="text-lg font-medium text-gray-900">Activity</h2>
                </div>
                <div class="pt-6">
                  <!-- Activity feed-->
                  <div class="flow-root">
                    <ul role="list" class="-mb-8">
                      <li v-for="(item, itemIdx) in activity" :key="item.id">
                        <div class="relative pb-8">
                          <span v-if="itemIdx !== activity.length - 1" class="absolute left-5 top-5 -ml-px h-full w-0.5 bg-gray-200" aria-hidden="true" />
                          <div class="relative flex items-start space-x-3">
                            <template v-if="item.type === 'comment'">
                              <div class="relative">
                                <img class="flex h-10 w-10 items-center justify-center rounded-full bg-gray-400 ring-8 ring-white" :src="item.imageUrl" alt="" />

                                <span class="absolute -bottom-0.5 -right-1 rounded-tl bg-white px-0.5 py-px">
                                  <ChatBubbleLeftEllipsisIcon class="h-5 w-5 text-gray-400" aria-hidden="true" />
                                </span>
                              </div>
                              <div class="min-w-0 flex-1">
                                <div>
                                  <div class="text-sm">
                                    <a :href="item.person.href" class="font-medium text-gray-900">{{ item.person.name }}</a>
                                  </div>
                                  <p class="mt-0.5 text-sm text-gray-500">Commented {{ item.date }}</p>
                                </div>
                                <div class="mt-2 text-sm text-gray-700">
                                  <p>{{ item.comment }}</p>
                                </div>
                              </div>
                            </template>
                            <template v-else-if="item.type === 'assignment'">
                              <div>
                                <div class="relative px-1">
                                  <div class="flex h-8 w-8 items-center justify-center rounded-full bg-gray-100 ring-8 ring-white">
                                    <UserCircleIconMini class="h-5 w-5 text-gray-500" aria-hidden="true" />
                                  </div>
                                </div>
                              </div>
                              <div class="min-w-0 flex-1 py-1.5">
                                <div class="text-sm text-gray-500">
                                  <a :href="item.person.href" class="font-medium text-gray-900">{{ item.person.name }}</a>
                                  {{ ' ' }}
                                  assigned
                                  {{ ' ' }}
                                  <a :href="item.assigned.href" class="font-medium text-gray-900">{{ item.assigned.name }}</a>
                                  {{ ' ' }}
                                  <span class="whitespace-nowrap">{{ item.date }}</span>
                                </div>
                              </div>
                            </template>
                            <template v-else>
                              <div>
                                <div class="relative px-1">
                                  <div class="flex h-8 w-8 items-center justify-center rounded-full bg-gray-100 ring-8 ring-white">
                                    <TagIcon class="h-5 w-5 text-gray-500" aria-hidden="true" />
                                  </div>
                                </div>
                              </div>
                              <div class="min-w-0 flex-1 py-0">
                                <div class="text-sm leading-8 text-gray-500">
                                  <span class="mr-0.5">
                                    <a :href="item.person.href" class="font-medium text-gray-900">{{ item.person.name }}</a>
                                    {{ ' ' }}
                                    added tags
                                  </span>
                                  {{ ' ' }}
                                  <span class="mr-0.5">
                                    <template v-for="tag in item.tags" :key="tag.name">
                                      <a :href="tag.href" class="relative inline-flex items-center rounded-full px-2.5 py-1 text-xs ring-1 ring-inset ring-gray-300 hover:bg-gray-50">
                                        <span class="absolute flex flex-shrink-0 items-center justify-center">
                                          <span :class="[tag.color, 'h-1.5 w-1.5 rounded-full']" aria-hidden="true" />
                                        </span>
                                        <span class="ml-3 font-semibold text-gray-900">{{ tag.name }}</span>
                                      </a>
                                      {{ ' ' }}
                                    </template>
                                  </span>
                                  <span class="whitespace-nowrap">{{ item.date }}</span>
                                </div>
                              </div>
                            </template>
                          </div>
                        </div>
                      </li>
                    </ul>
                  </div>
                </div>
              </div>
            </div>
          </section>
        </div>
        <aside class="hidden xl:block xl:pl-8">
          <h2 class="sr-only">Details</h2>
          <div class="space-y-5">
            <div class="flex items-center space-x-2">
              <LockOpenIcon class="h-5 w-5 text-green-500" aria-hidden="true" />
              <span class="text-sm font-medium text-green-700">Open Issue</span>
            </div>
            <div class="flex items-center space-x-2">
              <ChatBubbleLeftEllipsisIcon class="h-5 w-5 text-gray-400" aria-hidden="true" />
              <span class="text-sm font-medium text-gray-900">4 comments</span>
            </div>
            <div class="flex items-center space-x-2">
              <CalendarIcon class="h-5 w-5 text-gray-400" aria-hidden="true" />
              <span class="text-sm font-medium text-gray-900">Created on <time datetime="2020-12-02">Dec 2, 2020</time></span>
            </div>
          </div>
          <div class="mt-6 space-y-8 border-t border-gray-200 py-6">
            <div>
              <h2 class="text-sm font-medium text-gray-500">Assignees</h2>
              <ul role="list" class="mt-3 space-y-3">
                <li class="flex justify-start">
                  <a href="#" class="flex items-center space-x-3">
                    <div class="flex-shrink-0">
                      <img class="h-5 w-5 rounded-full" src="https://images.unsplash.com/photo-1520785643438-5bf77931f493?ixlib=rb-=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=facearea&facepad=8&w=256&h=256&q=80" alt="" />
                    </div>
                    <div class="text-sm font-medium text-gray-900">Eduardo Benz</div>
                  </a>
                </li>
              </ul>
            </div>
            <div>
              <h2 class="text-sm font-medium text-gray-500">Tags</h2>
              <ul role="list" class="mt-2 leading-8">
                <li class="inline">
                  <a href="#" class="relative inline-flex items-center rounded-full px-2.5 py-1 ring-1 ring-inset ring-gray-300 hover:bg-gray-50">
                    <div class="absolute flex flex-shrink-0 items-center justify-center">
                      <span class="h-1.5 w-1.5 rounded-full bg-rose-500" aria-hidden="true" />
                    </div>
                    <div class="ml-3 text-xs font-semibold text-gray-900">Bug</div>
                  </a>
                  {{ ' ' }}
                </li>
                <li class="inline">
                  <a href="#" class="relative inline-flex items-center rounded-full px-2.5 py-1 ring-1 ring-inset ring-gray-300 hover:bg-gray-50">
                    <div class="absolute flex flex-shrink-0 items-center justify-center">
                      <span class="h-1.5 w-1.5 rounded-full bg-indigo-500" aria-hidden="true" />
                    </div>
                    <div class="ml-3 text-xs font-semibold text-gray-900">Accessibility</div>
                  </a>
                  {{ ' ' }}
                </li>
              </ul>
            </div>
          </div>
        </aside>
      </div>
    </div>

</template>

<script setup lang="ts">
interface IssueDto {
  id: string;
  title: string;
  href: string;
  createdAt: Date;
  lastSeen: Date;
}

const issue: IssueDto = {
  id: '4b9f015a-2aab-4436-a0da-8dc09f080b68',
  title: 'Index was out of range. Must be non-negative and less than the size of the collection. (Parameter \'index\')',
  href: '#',
  createdAt: new Date(),
  lastSeen: new Date(),
};

</script>