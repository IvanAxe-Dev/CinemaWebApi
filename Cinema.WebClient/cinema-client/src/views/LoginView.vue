<script>
    import axios from 'axios';
    import { validateLoginData } from '@/utils';
    import { mapMutations, mapGetters, mapActions } from 'vuex';

    export default {
        data() {
            return {
                emailOrUsername: '',
                password: '',
                emailPlaceholder: 'Username or Email',
                passwordPlaceholder: 'Password',
                requestError: ''
            };
        },
        computed: {
            ...mapGetters(['isLoggedIn'])
        },
        methods: {
            ...mapMutations(['setLoggedIn']),
            ...mapActions(['authorize']),
            login() {
                const userData = {
                    emailOrUsername: this.emailOrUsername,
                    password: this.password
                };

                const invalidStatus = validateLoginData(userData);

                if (invalidStatus.invalid === true) {
                    if (invalidStatus.username) {
                        this.emailOrUsername = "";
                        this.emailPlaceholder = "Username must be 5-50 characters long";
                    }

                    if (invalidStatus.password) {
                        this.password = "";
                        this.passwordPlaceholder = "Invalid password";
                    }
                } else {
                    this.postData();
                }
            },
            postData() {
                axios({
                    method: 'post',
                    url: 'api/Account/login',
                    data: {
                        emailOrUsername: this.emailOrUsername,
                        password: this.password
                    }
                }).then(response => {
                    this.authorize({ 
                        tokenValue: response.data.token, 
                        emailValue: response.data.email 
                    });
                    this.$router.push('/');
                }).catch(error => {
                    if (error.response.data) {
                        const errorDetails = error.response.data.detail;
                        this.formatServerErrorResponse(errorDetails);
                    } else {
                        console.log(error);
                    }
                });
            },
            formatServerErrorResponse(errorDetails) {
                if (errorDetails === "Invalid Email/Username") {
                    this.emailOrUsername = '';
                    this.emailPlaceholder = errorDetails;
                }
                if (errorDetails === "Invalid Password") {
                    this.password = '';
                    this.passwordPlaceholder = errorDetails;
                } else {
                    this.emailOrUsername, this.password = '';
                    this.emailPlaceholder, this.passwordPlaceholder = errorDetails;
                }
            },
        }
    };
</script>

<template>
    <div>
        <div class="image-container">
            <div class="background">
                <div class="shape"></div>
                <div class="shape"></div>
            </div>
            <form @submit.prevent="login">
                <h3>Login Here</h3>

                <label for="username">Username</label>
                <input type="text" :placeholder="emailPlaceholder" id="username" v-model="emailOrUsername">

                <label for="password">Password</label>
                <input type="password" :placeholder="passwordPlaceholder" id="password" v-model="password">

                <button type="submit">Log In</button>

            </form>
        </div>
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
        height: 450px;
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

    input[type="text"],
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
</style>