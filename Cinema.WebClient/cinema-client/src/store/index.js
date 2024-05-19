import { createStore } from 'vuex'

export default createStore({
  state: {
    isLoggedIn: localStorage.getItem('isLoggedIn') === 'true'
  },
  getters: {
    isLoggedIn: state => state.isLoggedIn
  },
  mutations: {
    setLoggedIn(state, isLoggedIn) {
      state.isLoggedIn = isLoggedIn
      localStorage.setItem('isLoggedIn', isLoggedIn);
    }
  },
  actions: {
    authorize({ commit }) {
      commit('setLoggedIn', true)
    },
    logout({ commit }) {
      commit('setLoggedIn', false)
    }
  }
})