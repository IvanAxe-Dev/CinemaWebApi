<script>
import axios from 'axios';
export default {
    data() {
        return {
            username: '',
            email: '',
            password: '',
            repeatPassword: '',
            usernamePlaceholder: 'Username',
            emailPlaceholder: 'Email',
            passwordPlaceholder: 'Password',
            loading: false,
            errors: {}
        };
    },
    methods: {
        validate() {
            this.errors = {};
            if (!this.username) {
                this.errors.username = 'Username is required.';
                this.usernamePlaceholder = this.errors.username;
            }
            if (!this.email) {
                this.errors.email = 'Email is required.';
                this.emailPlaceholder = this.errors.email;
            } else if (!/\S+@\S+\.\S+/.test(this.email)) {
                this.errors.email = 'Email is invalid.';
                this.emailPlaceholder = this.errors.email;
            }
            if (!this.password) {
                this.errors.password = 'Password is required.';
                this.passwordPlaceholder = this.errors.password;
            }
            if (!this.repeatPassword) {
                this.errors.repeatPassword = 'Repeat Password is required.';
                this.passwordPlaceholder = this.errors.password;
            } 
            if (this.password !== this.repeatPassword) {
                this.errors.repeatPassword = 'Passwords do not match.';
                this.passwordPlaceholder = '';
            }
            console.log(Object.keys(this.errors).length)
            return Object.keys(this.errors).length === 0;
        },
        register() {
            if (!this.validate()) {
                return;
            }

            const userData = {
                username: this.username,
                email: this.email,
                password: this.password,
                repeatPassword: this.repeatPassword
            };

            axios({
                method: 'post',
                url: 'api/Account/register',
                data: userData
            }).then(response => {
                console.log(response);
                this.$router.push('/');

            }).catch(error => {
                console.error(error);
                this.formatServerErrorResponse(error.response.data.detail);

            });
        },
        formatServerErrorResponse(errorDetails) {
            this.username, this.email, this.password, this.repeatPassword = '';
            this.usernamePlaceholder, this.emailPlaceholder, this.passwordPlaceholder = errorDetails
        },
    }
};
</script>

<template>
    <div>
        <div class="background">
            <div class="shape"></div>
            <div class="shape"></div>
        </div>
        <form @submit.prevent="register">
            <h3>Register Here</h3>

            <label for="username">Username</label>
            <input type="text" :placeholder="usernamePlaceholder" id="username" v-model="username">

            <label for="email">Email</label>
            <input type="email" :placeholder="emailPlaceholder" id="email" v-model="email">

            <label for="password">Password</label>
            <input type="password" :placeholder="passwordPlaceholder" id="password" v-model="password">

            <label for="repeatPassword">Repeat Password</label>
            <input type="password" :placeholder="passwordPlaceholder" id="repeatPassword" v-model="repeatPassword">

            <button type="submit">Sign Up</button>

        </form>
    </div>
</template>

<style scoped>
*,
*:before,
*:after {
    padding: 0;
    margin: 0;
    box-sizing: border-box;
}

body {
    background-color: #080710;
    font-family: 'Poppins', sans-serif;
    color: #ffffff;
}

.background {
    width: 430px;
    height: 520px;
    position: absolute;
    transform: translate(-50%, -50%);
    left: 50%;
    top: 50%;
}

.background .shape {
    height: 200px;
    width: 200px;
    position: absolute;
    border-radius: 50%;
}

.shape:first-child {
    background: linear-gradient(#1845ad, #23a2f6);
    left: -80px;
    top: -80px;
}

.shape:last-child {
    background: linear-gradient(to right, #ff512f, #f09819);
    right: -30px;
    bottom: -80px;
}

form {
    height: 620px;
    width: 400px;
    background-color: rgba(255, 255, 255, 0.13);
    position: absolute;
    transform: translate(-50%, -50%);
    top: 50%;
    left: 50%;
    border-radius: 10px;
    backdrop-filter: blur(10px);
    border: 2px solid rgba(255, 255, 255, 0.1);
    box-shadow: 0 0 40px rgba(8, 7, 16, 0.6);
    padding: 50px 35px;
}

form h3 {
    font-size: 32px;
    font-weight: 500;
    line-height: 42px;
    text-align: center;
    margin-bottom: 30px;
}

label {
    display: block;
    font-size: 16px;
    font-weight: 500;
    margin-bottom: 10px;
}

input[type="text"],
input[type="email"],
input[type="password"] {
    height: 50px;
    width: 100%;
    background-color: rgba(255, 255, 255, 0.07);
    border-radius: 3px;
    padding: 0 10px;
    margin-bottom: 20px;
    font-size: 14px;
    font-weight: 300;
    color: #000000;
}

::placeholder {
    color: #e5e5e5;
}

button {
    width: 100%;
    background-color: #808080;
    color: #ffffff;
    padding: 15px 0;
    font-size: 18px;
    font-weight: 600;
    border-radius: 5px;
    cursor: pointer;
    border: none;
    margin-top: 20px;
}

button:hover {
    background-color: #a9a9a9;
}

.social {
    margin-top: 30px;
    display: flex;
    justify-content: center;
}

.social div {
    background: red;
    width: 150px;
    border-radius: 3px;
    padding: 5px 10px 10px 5px;
    background-color: rgba(255, 255, 255, 0.27);
    color: #eaf0fb;
    text-align: center;
    margin: 0 10px;
}

.social div:hover {
    background-color: rgba(255, 255, 255, 0.47);
}

.social i {
    margin-right: 4px;
}
</style>
