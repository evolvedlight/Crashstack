import { createApp } from 'vue'
import './style.css'
import App from './App.vue'

import { createWebHistory, createRouter } from 'vue-router'

import ProjectList from './views/ProjectList.vue'
import IssueList from './views/IssueList.vue'
import IssueView from './views/IssueView.vue'

const routes = [
  { path: '/project/:id', component: IssueList },
  { path: '/issue/:id', component: IssueView },
  { path: '/', component: ProjectList },
  
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

createApp(App).use(router).mount('#app')
