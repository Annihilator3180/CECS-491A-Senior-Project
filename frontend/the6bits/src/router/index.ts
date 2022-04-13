import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import homeShit from '@/components/Home.vue'
import LoginPost from '@/components/LoginPost.vue'
import OTPPost from '@/components/OneTimePass.vue'
import UM from '@/components/UM.vue'
import CreateGoal from '@/components/WeightManagement/CreateWeightGoal.vue'
import ResetPassword from '@/components/ResetPassword.vue'
import TrackingLog from '@/components/TrackingLog.vue'
import FavoriteDrugListPost from '@/components/FavoriteList.vue'
import RegistrationPost from '@/components/Registration.vue'
import MedSearch from '@/components/MedicationSearch.vue'
import viewHT from '@/components/viewHotTopics.vue'
import AccountRecovery from '@/components/AccountRecovery.vue'
import SearchFood from '@/components/WeightManagement/SearchFoodItem.vue'
import ViewDrug from '@/components/WeightManagement/viewDrug.vue'
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
    path: '/getTLogs',
    name: 'getTLogs',
    component: TrackingLog
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
    path: '/ResetPassword',
    name: 'ResetPassword',
    component: ResetPassword
  },
  {
    path:'/Registration',
    name:'Registration' ,
    component: RegistrationPost
},
{
  path:'/MedicationSearch',
  name: 'MedSearch',
  component: MedSearch
},
{
  path:'/HotTopics',
  name: 'HotTopics',
  component:viewHT
},
{
    path: '/SearchFood',
    name: 'SearchFood',
    component: SearchFood
  },
  {
    path: '/FavoriteDrug',
    name: 'FavoriteDrugListPost',
    component: FavoriteDrugListPost
  },
  {
    path: '/viewDrug/:id',
    name: 'ViewDrug',
    component: ViewDrug
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
