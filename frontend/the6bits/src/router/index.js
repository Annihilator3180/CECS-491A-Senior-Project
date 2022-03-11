
import {createRouter, createWebHistory} from 'vue-router'
import Registration from '../components/Registration.vue'

const routes=[
    {
        path:"/Register",
        name: "Register",
        component: Registration
    }
    
  ]
  const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
  })
  export default router