import axios from 'axios';

export const authorize = async ({ commit }, { tokenValue, emailValue }) => {
    commit('setAuthorizationToken', tokenValue)
    commit('setUserEmail', emailValue)

    console.log(tokenValue);
    console.log(emailValue);

    const url = `api/Account/ConfirmEmail?token=${tokenValue}&email=${emailValue}`;

    try {
        const response = await axios.get(url);
        commit('setAdmin', true)
        console.log(response)
    } catch (error) {
        console.log(error);
    }

    // For testing purposes
    commit('setAdmin', true)
    commit('setLoggedIn', true);
}

export const logout = ({ commit }) => {
    commit('setLoggedIn', false)
    commit('setAdmin', false)
    commit('setAuthorizationToken', '')
    commit('setUserEmail', '')
}