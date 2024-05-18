import { createRouter, createWebHashHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import AboutView from '@/views/AboutView.vue'
import LoginView from '@/views/LoginView.vue'
import RegisterView from '@/views/RegisterView.vue'
import AccountView from '@/views/AccountView.vue'
import MovieDetailsView from '@/views/MovieDetailsView.vue'
import AllMovies from '@/views/AdminPanel/Movies/AllMovies.vue'

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
    component: AccountView
  },
  {
    path: '/admin/movies',
    name: 'admin-movies',
    component: AllMovies,
  },
  {
    path: '/admin/movies/add',
    name: 'admin-movies',
    component: AllMovies,
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
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

router.beforeEach((to, from, next) => {
  if (to.meta.requiresAuth) {
    next({name: 'login'})
  } else {
    next();
  }
})

export default router
