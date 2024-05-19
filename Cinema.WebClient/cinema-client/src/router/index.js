    import { createRouter, createWebHashHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import AboutView from '@/views/AboutView.vue'
import LoginView from '@/views/LoginView.vue'
import RegisterView from '@/views/RegisterView.vue'
import AccountView from '@/views/AccountView.vue'
import MovieDetailsView from '@/views/MovieDetailsView.vue'
import AdminAddMovieView from '@/views/AdminAddMovieView.vue';

import store from '@/store'

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/movie/:id',
    name: 'movie-details',
    component: MovieDetailsView,
  },
  {
    path: '/account',
    name: 'account',
    meta: {
      requiresAuth: true
    },
    component: AccountView
  },
  {
    path: '/about',
    name: 'about',
    component: AboutView
  },
  {
    path: '/login',
    name: 'login',
    component: LoginView
  },
  {
    path: '/register',
    name: 'register',
    component: RegisterView
    },
  {
        path: '/admin',
        name: 'admin',
        meta: {
            requiresAuth: true
        },
      component: AdminAddMovieView
    },
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

router.beforeEach((to, from, next) => {
  const requiresAuth = to.matched.some(record => record.meta.requiresAuth)
  const isLoggedIn = store.getters.isLoggedIn

  if (requiresAuth && !isLoggedIn) {
    next('/login')
  } else {
    next()
  }
})

export default router
