import { createStore } from 'vuex'
import * as actions from './actions'

export default createStore({
  state: {
    isLoggedIn: localStorage.getItem('isLoggedIn') === 'false',
    isAdmin: localStorage.getItem('isAdmin') === 'false',
    authorizationToken: localStorage.getItem('authorizationToken') === '',
    userEmail: localStorage.getItem('userEmail') === ''
  },
  getters: {
    isLoggedIn: state => state.isLoggedIn,
    isAdmin: state => state.isAdmin,
    authorizationToken: state => state.authorizationToken,
    userEmail: state => state.userEmail,
  },
  mutations: {
    setLoggedIn(state, isLoggedIn) {
      state.isLoggedIn = isLoggedIn;
      localStorage.setItem('isLoggedIn', isLoggedIn);
    },
    setAuthorizationToken(state, authorizationToken) {
      state.authorizationToken = authorizationToken;
      localStorage.setItem('authorizationToken', authorizationToken);
    },
    setUserEmail(state, userEmail) {
      state.userEmail = userEmail;
      localStorage.setItem('userEmail', userEmail);
    },
    setAdmin(state, isAdmin) {
      state.isAdmin = isAdmin;
      localStorage.setItem('isAdmin', isAdmin);
    }
  },
  actions
})