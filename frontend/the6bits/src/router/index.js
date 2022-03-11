import {createRouter, createWebHistory} from 'vue-router'

const routes=[
    {
        path:"/Register",
        name: "Register",
        component: './components/Registration.vue'
    }
  ]
  const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
  })
  export default router