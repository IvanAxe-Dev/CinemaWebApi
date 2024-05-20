import { createStore } from 'vuex'
import * as actions from './actions'

export default createStore({
  state: {
    isLoggedIn: localStorage.getItem('isLoggedIn') === 'true',
    authorizationToken: localStorage.getItem('authorizationToken') === '',
    userEmail: localStorage.getItem('userEmail') === ''
  },
  getters: {
    isLoggedIn: state => state.isLoggedIn,
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
    }
  },
  actions
})