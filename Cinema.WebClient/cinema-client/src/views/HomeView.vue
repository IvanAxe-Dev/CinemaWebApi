<script setup>
import MovieCard from '../components/MovieCard.vue';

const movies = [
  {
    id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    title: "string",
    rentalStartDate: "2024-05-15T07:09:52.280Z",
    rentalEndDate: "2024-05-15T07:09:52.280Z",
    description: "string",
    imageUrl: "https://via.placeholder.com/150",
    releaseDate: "2024-05-15T07:09:52.280Z",
    director: "string",
    duration: "string",
    ageRestriction: 0,
    trailerUrl: "string",
    actors: "string",
    ratings: [
      0
    ],
    sessions: [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "date": "string",
        "startTime": "10:00",
        "price": 0,
        "availableSeats": 0,
        "movieId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "cinemaHallId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
      },
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "date": "string",
        "startTime": "12:00",
        "price": 0,
        "availableSeats": 1,
        "movieId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "cinemaHallId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
      },
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "date": "string",
        "startTime": "14:30",
        "price": 0,
        "availableSeats": 1,
        "movieId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "cinemaHallId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
      },
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "date": "string",
        "startTime": "16:45",
        "price": 0,
        "availableSeats": 1,
        "movieId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "cinemaHallId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
      },
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "date": "string",
        "startTime": "19:00",
        "price": 0,
        "availableSeats": 1,
        "movieId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "cinemaHallId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
      }
    ],
    categories: [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "name": "string"
      }
    ]
  },
  {
    id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    title: "string",
    rentalStartDate: "2024-05-15T07:09:52.280Z",
    rentalEndDate: "2024-05-15T07:09:52.280Z",
    description: "string",
    imageUrl: "https://via.placeholder.com/150",
    releaseDate: "2024-05-15T07:09:52.280Z",
    director: "string",
    duration: "string",
    ageRestriction: 0,
    trailerUrl: "string",
    actors: "string",
    ratings: [
      0
    ],
    sessions: [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "date": "string",
        "startTime": "string",
        "price": 0,
        "availableSeats": 0,
        "movieId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "cinemaHallId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
      }
    ],
    categories: [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "name": "string"
      }
    ]
  },
  {
    id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    title: "string",
    rentalStartDate: "2024-05-15T07:09:52.280Z",
    rentalEndDate: "2024-05-15T07:09:52.280Z",
    description: "string",
    imageUrl: "https://via.placeholder.com/150",
    releaseDate: "2024-05-15T07:09:52.280Z",
    director: "string",
    duration: "string",
    ageRestriction: 0,
    trailerUrl: "string",
    actors: "string",
    ratings: [
      0
    ],
    sessions: [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "date": "string",
        "startTime": "string",
        "price": 0,
        "availableSeats": 0,
        "movieId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "cinemaHallId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
      }
    ],
    categories: [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "name": "string"
      }
    ]
  },
  {
    id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    title: "string",
    rentalStartDate: "2024-05-15T07:09:52.280Z",
    rentalEndDate: "2024-05-15T07:09:52.280Z",
    description: "string",
    imageUrl: "https://via.placeholder.com/150",
    releaseDate: "2024-05-15T07:09:52.280Z",
    director: "string",
    duration: "string",
    ageRestriction: 0,
    trailerUrl: "string",
    actors: "string",
    ratings: [
      0
    ],
    sessions: [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "date": "string",
        "startTime": "string",
        "price": 0,
        "availableSeats": 0,
        "movieId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "cinemaHallId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
      }
    ],
    categories: [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "name": "string"
      }
    ]
  },
  {
    id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    title: "string",
    rentalStartDate: "2024-05-15T07:09:52.280Z",
    rentalEndDate: "2024-05-15T07:09:52.280Z",
    description: "string",
    imageUrl: "https://via.placeholder.com/150",
    releaseDate: "2024-05-15T07:09:52.280Z",
    director: "string",
    duration: "string",
    ageRestriction: 0,
    trailerUrl: "string",
    actors: "string",
    ratings: [
      0
    ],
    sessions: [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "date": "string",
        "startTime": "string",
        "price": 0,
        "availableSeats": 0,
        "movieId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "cinemaHallId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
      }
    ],
    categories: [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "name": "string"
      }
    ]
  },
  {
    id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    title: "string",
    rentalStartDate: "2024-05-15T07:09:52.280Z",
    rentalEndDate: "2024-05-15T07:09:52.280Z",
    description: "string",
    imageUrl: "https://via.placeholder.com/150",
    releaseDate: "2024-05-15T07:09:52.280Z",
    director: "string",
    duration: "string",
    ageRestriction: 0,
    trailerUrl: "string",
    actors: "string",
    ratings: [
      0
    ],
    sessions: [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "date": "string",
        "startTime": "string",
        "price": 0,
        "availableSeats": 0,
        "movieId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "cinemaHallId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
      }
    ],
    categories: [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "name": "string"
      }
    ]
  },
]
</script>

<template>
  <div class="home">
    <h1>Movies</h1>
    <div class="movie-grid">
      <movie-card v-for="movie in movies" :key="movie.id" :movie="movie" />
    </div>
  </div>
</template>

<style scoped>

.movie-grid {
  padding: 20px;
  padding-top: 30px;
  justify-content: center;
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(300px, 300px));
  gap: 20px;
}
</style>
