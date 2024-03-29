import { createRouter, createWebHashHistory, createWebHistory, RouteRecordRaw } from 'vue-router'
import HomeHome from '@/components/Home.vue'
import LoginPost from '@/components/LoginPost.vue'
import OTPPost from '@/components/OneTimePass.vue'
import UM from '@/components/UM.vue'
import GoalView from '@/components/WeightManagement/WeightGoalView.vue'
import ResetPassword from '@/components/ResetPassword.vue'
import TrackingLog from '@/components/TrackingLog.vue'
import CreateHealthRecord from '@/components/HealthRecorder/CreateHealthRecord.vue'
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
import RecipeFavorite from '@/components/RecipeFavorite.vue'
import logOut from '@/components/logout.vue'
import SaveFoodLog from '@/components/WeightManagement/SaveFoodLog.vue'
import viewReminder from '@/components/Reminders/ViewReminder.vue'
import analysisDash from '@/components/analysisDash.vue'
import VerifyAccount from '@/components/VerifyAccount.vue'
import FoodLogView from '@/components/WeightManagement/FoodLogView.vue'
import ViewMedicalRecords from '@/components/HealthRecorder/ViewHealthRecord.vue'
import EditMedicalRecords from '@/components/HealthRecorder/EditHealthRecord.vue'
import HealthRecorderHome from '@/components/HealthRecorder/HealthRecorderHome.vue'
import CreateReminder from '@/components/Reminders/CreateReminder.vue'
import DeleteReminder from '@/components/Reminders/DeleteReminder.vue'
import EditReminder from '@/components/Reminders/EditReminder.vue'
import NutritionAnalysis from '@/components/NutritionAnalysis.vue'
import BMICalculator from '@/components/BMICalculator.vue'
import FAQ from '@/components/FAQ.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'home',
    component: HomeHome
  },
  {
    path: '/login',
    name: 'login',
    component: LoginPost
  },
  {
    path: '/logout',
    name: 'logout',
    component: logOut
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
    component: GoalView,
    meta: {
      requiresAuth: true,
   },
  },
  {
    path: '/ResetPassword/:token:/:id',
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
    component: DeleteAccount,
    meta: {
    requiresAuth: true,
      },
  },

  {
    path: '/DietRecommendation',
    name: 'DietRecommendation',
    component: DietRecommendation,
    meta: {
          requiresAuth: true,
      },
  },
  {
    path: '/CreateHealthRecord',
    name: 'CreateHealthRecord',
    component: CreateHealthRecord,
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
    path: '/VerifyAccount/:token:/:id',
    name: 'VerifyAccount',
    component: VerifyAccount
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
    path: "/analysis",
  name: "Analysis Dash",
  component: analysisDash,
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
  },
  {
  path: "/FoodLog",
  name: "FoodLog",
  component: FoodLogView,
  meta: {
       requiresAuth: true,
    },
  },
  {
    path: "/ViewMedicalRecords",
    name: "ViewMedicalRecords",
    component: ViewMedicalRecords,
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: "/EditMedicalRecords",
    name: "EditMedicalRecords",
    component: EditMedicalRecords,
    meta: {
      requiresAuth: true,
    }
  },
    {
      path: "/HealthRecorderHome",
      name: "HealthRecorderHome",
      component: HealthRecorderHome
   },
   {
    path: "/CreateReminder",
    name: "CreateReminder",
    component: CreateReminder,
    meta: {
         requiresAuth: true,
      },
    },
    {
      path: "/DeleteReminder",
      name: "DeleteReminder",
      component: DeleteReminder,
      meta: {
           requiresAuth: true,
        },
      },
      {
        path: "/EditReminder",
        name: "EditReminder",
        component: EditReminder,
        meta: {
             requiresAuth: true,
          },
        },

    {
        path: '/NutritionAnalysis',
        name: 'NutritionAnalysis',
        component: NutritionAnalysis,
         meta: {
            requiresAuth: true,
        },
    },
    {
        path: '/BMICalculator',
        name: 'BMICalculator',
        component: BMICalculator,
        meta: {
            requiresAuth: true,
        },
    },

    {
        path: "/RecipeFavorite",
        name: "RecipeFavorite",
        component: RecipeFavorite,
        meta: {
        requiresAuth: true,
        }
    },
    {
        path: "/FAQ",
        name: "FAQ",
        component: FAQ,
        meta: {
        requiresAuth: false,
        }
    }



]

const router = createRouter({
  history: createWebHashHistory(),
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
