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
    component: UM,
    meta: {
         requiresAuth: true,
      },
  },
  {
    path: '/getTLogs',
    name: 'getTLogs',
    component: TrackingLog,
    meta: {
         requiresAuth: true,
      },
  },
  {
    path: '/AccountRecovery',
    name: 'AccountRecovery',
    component: AccountRecovery,
    meta: {
         requiresAuth: true,
      },

  },
  {
    path: '/WeightManagement',
    name: 'WeightManagement',
    component: GoalView,
    meta: {
         requiresAuth: true,
      },
  },
  {
    path: '/ResetPassword',
    name: 'ResetPassword',
    component: ResetPassword,
    meta: {
         requiresAuth: true,
      },
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
    component: viewHT,
    meta: {
         requiresAuth: true,
      },
  },
  {
    path: '/SearchFood',
    name: 'SearchFood',
    component: SearchFood,
    meta: {
         requiresAuth: true,
      },
  },
  {
    path: '/DeleteAccount',
    name: 'DeleteAccount',
    component: DeleteAccount,
    meta: {
         requiresAuth: true,
      },
  },

  {
    path: '/DietRecommendation',
    name: 'DietRecommendation',
    component: DietRecommendation
  },
  {
    path: '/HealthRecorder',
    name: 'HealthRecorder',
    component: HealthRecorder,
    meta: {
         requiresAuth: true,
      },
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
  }

]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})



router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (sessionStorage.getItem('token') == null) {
      next({
        name: 'login',
      })
    } else {      
        next()
    }
  
  } else {
    next()
  }
})





export default router
