<template>
    <div id="app">
        <h1>{{ movie.id ? 'Edit Movie' : 'Add Movie' }}</h1>
        <form @submit.prevent="submitMovie" class="movie-form">
            <h2>Movie ID</h2>
            <div class="form-group">
                <label for="id">ID:</label>
                <input v-model="movie.id" type="text" id="id" name="id" class="form-control" />
            </div>
            <h2>Search and Categories</h2>
            <div class="form-group">
                <label for="searchTerm">Search Term:</label>
                <input v-model="movie.searchTerm" type="text" id="searchTerm" name="searchTerm" required class="form-control" />
            </div>
            <div class="form-group">
                <label for="categories">Categories:</label>
                <input v-model="movie.categories" type="text" id="categories" name="categories" required class="form-control" />
            </div>
            <h2>Date and Time Intervals</h2>
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
            <h2>Movie Details</h2>
            <div class="form-group">
                <label for="title">Title:</label>
                <input v-model="movie.title" type="text" id="title" name="title" required class="form-control" />
            </div>
            <div class="form-group">
                <label for="rentalStartDate">Rental Start Date:</label>
                <input v-model="movie.rentalStartDate" type="datetime-local" id="rentalStartDate" name="rentalStartDate" required class="form-control" />
            </div>
            <div class="form-group">
                <label for="rentalEndDate">Rental End Date:</label>
                <input v-model="movie.rentalEndDate" type="datetime-local" id="rentalEndDate" name="rentalEndDate" required class="form-control" />
            </div>
            <div class="form-group">
                <label for="description">Description:</label>
                <input v-model="movie.description" type="text" id="description" name="description" required class="form-control" />
            </div>
            <div class="form-group">
                <label for="imageUrl">Image URL:</label>
                <input v-model="movie.imageUrl" type="text" id="imageUrl" name="imageUrl" required class="form-control" />
            </div>
            <div class="form-group">
                <label for="releaseDate">Release Date:</label>
                <input v-model="movie.releaseDate" type="datetime-local" id="releaseDate" name="releaseDate" required class="form-control" />
            </div>
            <div class="form-group">
                <label for="director">Director:</label>
                <input v-model="movie.director" type="text" id="director" name="director" required class="form-control" />
            </div>
            <div class="form-group">
                <label for="duration">Duration:</label>
                <input v-model="movie.duration" type="text" id="duration" name="duration" required class="form-control" />
            </div>
            <div class="form-group">
                <label for="ageRestriction">Age Restriction:</label>
                <input v-model="movie.ageRestriction" type="number" id="ageRestriction" name="ageRestriction" required class="form-control" />
            </div>
            <div class="form-group">
                <label for="trailerUrl">Trailer URL:</label>
                <input v-model="movie.trailerUrl" type="text" id="trailerUrl" name="trailerUrl" required class="form-control" />
            </div>
            <div class="form-group">
                <label for="actors">Actors:</label>
                <input v-model="movie.actors" type="text" id="actors" name="actors" required class="form-control" />
            </div>
            <button type="submit" class="btn btn-primary">{{ movie.id ? 'Update' : 'Add' }} Movie</button>
        </form>

        <div v-if="showPopup" class="popup">
            <div class="popup-content">
                <h3>{{ movie.id ? 'Update Movie' : 'Add Movie' }}</h3>
                <p>{{ popupMessage }}</p>
                <button @click="closePopup">Close</button>
            </div>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        data() {
            return {
                movie: {
                    id: "",
                    searchTerm: "",
                    categories: "",
                    dateStartInterval: "",
                    dateEndInterval: "",
                    timeStartInterval: "",
                    timeEndInterval: "",
                    title: "",
                    rentalStartDate: "",
                    rentalEndDate: "",
                    description: "",
                    imageUrl: "",
                    releaseDate: "",
                    director: "",
                    duration: "",
                    ageRestriction: 0,
                    trailerUrl: "",
                    actors: ""
                },
                showPopup: false,
                popupMessage: "",
            };
        },
        methods: {
            submitMovie() {
                if (this.movie.id) {
                    axios.put(`/api/Movie/${this.movie.id}`, {
                        title: this.movie.title,
                        rentalStartDate: this.movie.rentalStartDate,
                        rentalEndDate: this.movie.rentalEndDate,
                        description: this.movie.description,
                        imageUrl: this.movie.imageUrl,
                        releaseDate: this.movie.releaseDate,
                        director: this.movie.director,
                        duration: this.movie.duration,
                        ageRestriction: this.movie.ageRestriction,
                        trailerUrl: this.movie.trailerUrl,
                        actors: this.movie.actors
                    })
                        .then(response => {
                            console.log("Movie updated:", response.data);
                            this.popupMessage = "Movie updated successfully.";
                            this.showPopup = true;
                            this.clearForm();
                        })
                        .catch(error => {
                            console.error('Error updating movie:', error);
                            this.popupMessage = "An error occurred while updating the movie.";
                            this.showPopup = true;
                        });
                } else {
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
                            this.popupMessage = "Movie data fetched successfully.";
                            this.showPopup = true;
                            this.clearForm();
                        })
                        .catch(error => {
                            console.error('Error fetching movie data:', error);
                            this.popupMessage = "An error occurred while fetching movie data.";
                            this.showPopup = true;
                        });
                }
            },
            clearForm() {
                this.movie = {
                    id: "",
                    searchTerm: "",
                    categories: "",
                    dateStartInterval: "",
                    dateEndInterval: "",
                    timeStartInterval: "",
                    timeEndInterval: "",
                    title: "",
                    rentalStartDate: "",
                    rentalEndDate: "",
                    description: "",
                    imageUrl: "",
                    releaseDate: "",
                    director: "",
                    duration: "",
                    ageRestriction: 0,
                    trailerUrl: "",
                    actors: ""
                };
            },
            closePopup() {
                this.showPopup = false;
            }
        }
    };
</script>

<style scoped>
    .form-group {
        margin-bottom: 15px;
    }

    .popup {
        position: fixed;
        top: 0;
        left: 0;
        right: 0;
        bottom: 0;
        background: rgba(0, 0, 0, 0.5);
        display: flex;
        justify-content: center;
        align-items: center;
    }

    .popup-content {
        background: white;
        padding: 20px;
        border-radius: 5px;
        text-align: center;
    }
</style>
