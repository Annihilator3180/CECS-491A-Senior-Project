import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import homeShit from '@/components/Home.vue'
import LoginPost from '@/components/LoginPost.vue'
import OTPPost from '@/components/OneTimePass.vue'
import UM from '@/components/UM.vue'
import GoalView from '@/components/WeightManagement/WeightGoalView.vue'
import ResetPassword from '@/components/ResetPassword.vue'
import TrackingLog from '@/components/TrackingLog.vue'
import HealthRecorder from '@/components/HealthRecorder.vue'
import FavoriteDrugListPost from '@/components/FavoriteList.vue'
import RegistrationPost from '@/components/Registration.vue'
import MedSearch from '@/components/MedicationSearch.vue'
import viewHT from '@/components/viewHotTopics.vue'
import AccountRecovery from '@/components/AccountRecovery.vue'
import SearchFood from '@/components/WeightManagement/SearchFoodItem.vue'
import ViewDrug from '@/components/WeightManagement/viewDrug.vue'
import EditFavoriteDrug from '@/components/EditFavoriteDrug.vue'
import DeleteAccount from '@/components/DeleteAccount.vue'
import DietRecommendation from '@/components/DietRecommendation.vue'
import FavoriteList from '@/components/FavoriteList.vue'
import SaveFoodLog from '@/components/WeightManagement/SaveFoodLog.vue'
import viewReminder from '@/components/Reminders/ViewReminder.vue'

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
    component: GoalView
  },
  {
    path: '/ResetPassword',
    name: 'ResetPassword',
    component: ResetPassword
  },
  {
    path: '/Registration',
    name: 'Registration',
    component: RegistrationPost
  },
  {
    path: '/MedicationSearch',
    name: 'MedSearch',
    component: MedSearch,
    meta: {
      requiresAuth: true,
   },
  },
  {
    path: '/HotTopics',
    name: 'HotTopics',
    component: viewHT
  },
  {
    path: '/SearchFood',
    name: 'SearchFood',
    component: SearchFood
  },
  {
    path: '/DeleteAccount',
    name: 'DeleteAccount',
    component: DeleteAccount
  },

  {
    path: '/DietRecommendation',
    name: 'DietRecommendation',
    component: DietRecommendation
  },
  {
    path: '/HealthRecorder',
    name: 'HealthRecorder',
    component: HealthRecorder
  },
  {
      path: "/FavoriteDrugs",
    name: "FavoriteDrugListPost",
    component: FavoriteDrugListPost,
    meta: {
      requiresAuth: true,
   },
  },
  {
    path: '/viewDrug/:id',
    name: 'ViewDrug',
    component: ViewDrug,
    meta: {
      requiresAuth: true,
   },
  },
  {
    path: '/editFavoriteDrug/:id',
    name: 'EditDrug',
    component: EditFavoriteDrug,
    meta: {
         requiresAuth: true,
      },
  },
  {
      path: "/SaveFoodLog",
    name: "SaveFoodLog",
    component: SaveFoodLog,
    meta: {
         requiresAuth: true,
      },
  },
  {
  path: "/viewReminder",
  name: "viewReminder",
  component: viewReminder,
  meta: {
       requiresAuth: true,
    },
  }

]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
