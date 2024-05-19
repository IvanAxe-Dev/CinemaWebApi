import { createStore } from 'vuex'

export default createStore({
  state: {
    isLoggedIn: false
  },
  getters: {
    isLoggedIn: state => state.isLoggedIn
  },
  mutations: {
    setLoggedIn(state, isLoggedIn) {
      state.isLoggedIn = isLoggedIn
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
