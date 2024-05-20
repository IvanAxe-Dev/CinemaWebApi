import axios from 'axios';

export const authorize = async ({ commit }, { tokenValue, emailValue }) => {
    commit('setAuthorizationToken', tokenValue)
    commit('setUserEmail', emailValue)

    const url = `api/Account/ConfirmEmail?token=${tokenValue}&email=${emailValue}`;

    try {
        const response = await axios.get(url);
        commit('setAdmin', response.data.status)
        commit('setLoggedIn', true);
    } catch (error) {
        console.log(error);
    }

    commit('setAdmin', true)
    commit('setLoggedIn', true);
}

export const logout = ({ commit }) => {
    commit('setLoggedIn', false)
    commit('setAdmin', false)
    commit('setAuthorizationToken', '')
    commit('setUserEmail', '')
}