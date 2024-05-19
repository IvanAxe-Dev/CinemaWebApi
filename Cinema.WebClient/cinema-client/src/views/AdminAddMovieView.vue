
<template>
    <div id="app">
        <h1>{{ movie.id ? 'Edit Movie' : 'Add Movie' }}</h1>
        <button @click="showPutFields">PUT</button>
        <button @click="showGetFields">GET</button>
        <form v-if="putVisible" @submit.prevent="submitMovie" class="movie-form">
            <!-- Put -->
            <h2>Movie ID</h2>
            <div class="form-group">
                <label for="id">ID:</label>
                <input v-model="movie.id" type="text" id="id" name="id" class="form-control" />
            </div>
            <div class="form-group">
                <label for="title">Title:</label>
                <input v-model="movie.title" type="text" id="title" name="title" class="form-control" />
            </div>
            <div class="form-group">
                <label for="rentalStartDate">Rental Start Date:</label>
                <input v-model="movie.rentalStartDate" type="datetime-local" id="rentalStartDate" name="rentalStartDate" class="form-control" />
            </div>
            <div class="form-group">
                <label for="rentalEndDate">Rental End Date:</label>
                <input v-model="movie.rentalEndDate" type="datetime-local" id="rentalEndDate" name="rentalEndDate" class="form-control" />
            </div>
            <div class="form-group">
                <label for="description">Description:</label>
                <input v-model="movie.description" type="text" id="description" name="description" class="form-control" />
            </div>
            <div class="form-group">
                <label for="imageUrl">Image URL:</label>
                <input v-model="movie.imageUrl" type="text" id="imageUrl" name="imageUrl" class="form-control" />
            </div>
            <div class="form-group">
                <label for="releaseDate">Release Date:</label>
                <input v-model="movie.releaseDate" type="datetime-local" id="releaseDate" name="releaseDate" class="form-control" />
            </div>
            <div class="form-group">
                <label for="director">Director:</label>
                <input v-model="movie.director" type="text" id="director" name="director" class="form-control" />
            </div>
            <div class="form-group">
                <label for="duration">Duration:</label>
                <input v-model="movie.duration" type="text" id="duration" name="duration" class="form-control" />
            </div>
            <div class="form-group">
                <label for="ageRestriction">Age Restriction:</label>
                <input v-model="movie.ageRestriction" type="number" id="ageRestriction" name="ageRestriction" class="form-control" />
            </div>
            <div class="form-group">
                <label for="trailerUrl">Trailer URL:</label>
                <input v-model="movie.trailerUrl" type="text" id="trailerUrl" name="trailerUrl" class="form-control" />
            </div>
            <div class="form-group">
                <label for="actors">Actors:</label>
                <input v-model="movie.actors" type="text" id="actors" name="actors" class="form-control" />
            </div>
            <!-- Get -->
            <button type="submit" class="btn btn-primary">{{ movie.id ? 'Update' : 'Add' }} Movie</button>
        </form>
        <form v-if="getVisible" @submit.prevent="submitMovie" class="movie-form">
            <h2>Search and Categories</h2>
            <div class="form-group">
                <label for="searchTerm">Search Term:</label>
                <input v-model="movie.searchTerm" type="text" id="searchTerm" name="searchTerm" class="form-control" />
            </div>
            <div class="form-group">
                <label for="categories">Categories:</label>
                <input v-model="movie.categories" type="text" id="categories" name="categories" class="form-control" />
            </div>
            <div class="form-group">
                <label for="dateStartInterval">Date Start Interval:</label>
                <input v-model="movie.dateStartInterval" type="datetime-local" id="dateStartInterval" name="dateStartInterval" class="form-control" />
            </div>
            <div class="form-group">
                <label for="dateEndInterval">Date End Interval:</label>
                <input v-model="movie.dateEndInterval" type="datetime-local" id="dateEndInterval" name="dateEndInterval" class="form-control" />
            </div>
            <div class="form-group">
                <label for="timeStartInterval">Time Start Interval:</label>
                <input v-model="movie.timeStartInterval" type="time" id="timeStartInterval" name="timeStartInterval" class="form-control" />
            </div>
            <div class="form-group">
                <label for="timeEndInterval">Time End Interval:</label>
                <input v-model="movie.timeEndInterval" type="time" id="timeEndInterval" name="timeEndInterval" class="form-control" />
            </div>
           
            <button type="submit" class="btn btn-primary">Get Movie Data</button>
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
                putVisible: false,
                getVisible: false
            };
        },
        methods: {
            submitMovie() {
                if (this.putVisible) {
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
                } else if (this.getVisible) {
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
            showPutFields() {
                this.putVisible = true;
                this.getVisible = false;
                this.clearForm();
            },
            showGetFields() {
                this.putVisible = false;
                this.getVisible = true;
                this.clearForm();
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
   
</style>
