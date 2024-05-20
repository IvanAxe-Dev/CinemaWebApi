<template>
    <div id="app">
        <h1>Add Movie</h1>
        <form @submit.prevent="submitMovie" class="movie-form">
            <div class="form-group">
                <label for="searchTerm">Search Term:</label>
                <input v-model="movie.searchTerm" type="text" id="searchTerm" name="searchTerm" required class="form-control" />
            </div>
            <div class="form-group">
                <label for="categories">Categories:</label>
                <input v-model="movie.categories" type="text" id="categories" name="categories" required class="form-control" />
            </div>
            <div class="form-group">
                <label for="dateStartInterval">Date Start Interval:</label>
                <input v-model="movie.dateStartInterval" type="datetime-local" id="dateStartInterval" name="dateStartInterval" required class="form-control" />
            </div>
            <div class="form-group">
                <label for="dateEndInterval">Date End Interval:</label>
                <input v-model="movie.dateEndInterval" type="datetime-local" id="dateEndInterval" name="dateEndInterval" required class="form-control" />
            </div>
            <div class="form-group">
                <label for="timeStartInterval">Time Start Interval:</label>
                <input v-model="movie.timeStartInterval" type="time" id="timeStartInterval" name="timeStartInterval" required class="form-control" />
            </div>
            <div class="form-group">
                <label for="timeEndInterval">Time End Interval:</label>
                <input v-model="movie.timeEndInterval" type="time" id="timeEndInterval" name="timeEndInterval" required class="form-control" />
            </div>
            <button type="submit" class="btn btn-primary">Add Movie</button>
        </form>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        data() {
            return {
                movie: {
                    searchTerm: "",
                    categories: "",
                    dateStartInterval: "",
                    dateEndInterval: "",
                    timeStartInterval: "",
                    timeEndInterval: "",
                },
            };
        },
        methods: {
            submitMovie() {
                // Perform actions to add the movie (e.g., call API, store locally)
                axios.get('/api/Movie', {
                    params: {
                        SearchTerm: this.movie.searchTerm,
                        Categories: this.movie.categories,
                        DateStartInterval: this.movie.dateStartInterval,
                        DateEndInterval: this.movie.dateEndInterval,
                        TimeStartInterval: this.movie.timeStartInterval,
                        TimeEndInterval: this.movie.timeEndInterval,
                    }
                })
                    .then(response => {
                        console.log("Movie data:", response.data);
                        // Clear the form after successful submission
                        this.clearForm();
                    })
                    .catch(error => {
                        console.error('Error adding movie:', error);
                    });
            },
            clearForm() {
                // Clear the form fields
                this.movie.searchTerm = "";
                this.movie.categories = "";
                this.movie.dateStartInterval = "";
                this.movie.dateEndInterval = "";
                this.movie.timeStartInterval = "";
                this.movie.timeEndInterval = "";
            },
        },
    };
</script>

<style scoped>
    .form-group {
        margin-bottom: 15px;
    }
</style>
