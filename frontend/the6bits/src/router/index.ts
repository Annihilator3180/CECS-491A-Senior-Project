import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import homeShit from '@/components/Home.vue'
import LoginPost from '@/components/LoginPost.vue'
import OTPPost from '@/components/OneTimePass.vue'
import UM from '@/components/UM.vue'
import CreateGoal from '@/components/WeightManagement/CreateWeightGoal.vue'
import RegistrationGet from '@/components/Registration.vue'
import MedSearch from '@/components/MedicationSearch.vue'

import AccountRecovery from '@/components/AccountRecovery.vue'
const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'home',
    component: homeShit
  },
  {
    path: '/login',
    name: 'login',
    component: LoginPost
  },
  {
    path: '/otp',
    name: 'otp',
    component: OTPPost
  },
  {
    path: '/UM',
    name: 'UM',
    component: UM
  },
  {
    path: '/AccountRecovery',
    name: 'AccountRecovery',
    component: AccountRecovery
  },
  {
    path: '/WeightManagement',
    name: 'WeightManagement',
    component: CreateGoal
  },
  {
    path:'/Registration',
    name:'Registration' ,
    component: RegistrationGet
},
{
  path:'/MedicationSearch',
  name: 'MedSearch',
  component: MedSearch
}
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router