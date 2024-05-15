<script setup>
import { defineProps, computed } from 'vue';

const props = defineProps({
  movie: {
    type: Object,
    required: true
  }
});

const placeholderPoster = "https://via.placeholder.com/810x1200";
const imageUrlRegex = /(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|gif|png)/g;

const imageUrl = computed(() => {
  const isValidImageUrl = imageUrlRegex.test(props.movie.imageUrl);
  return isValidImageUrl ? props.movie.imageUrl : placeholderPoster;
});

const availableSessions = computed(() => {
  return props.movie.sessions.filter(session => session.availableSeats > 0);
});

const ratingMean = computed(() => {
  if (!props.movie.ratings) return 0;

  const ratingArray = props.movie.ratings;

  if (!ratingArray.length > 0) return 0;

  const sum = ratingArray.reduce((total, current) => total + current, 0);
  return (sum / ratingArray.length);
});

function formatDate(dateString) {
  const date = new Date(dateString);
  const day = date.getDay();
  const month = date.toLocaleString('default', { month: 'long'});
  return `${day} ${month}`;
}

const rentalTerm = computed(() => {
  const startDate = props.movie.rentalStartDate;
  const endDate = props.movie.rentalEndDate;

  if (startDate === endDate) {
    return formatDate(startDate);
  }

  return `${formatDate(startDate)} - ${formatDate(endDate)}`;
});

</script>

<template>
  <div class="movie-item">
    <div class="poster-wrapper">
      <img :src="imageUrl" alt="Movie Poster" class="movie-poster">
      <div class="movie-info-card">
        <div class="info-card-content">
          <div class="info-card-row">
            <div class="rating">
              Rating: {{ ratingMean }}/10
            </div>
            <div class="age-restriction">
              {{ props.movie.ageRestriction }}+
            </div>
          </div>
          <div class="info-card-row">
            <div class="row-heading">
              Genre:
            </div>
          </div>
          <div class="genre-grid">
            <div class="genre" v-for="category in props.movie.categories" :key="category.id">
              {{ category.name }}
            </div>
          </div>
          <div class="info-card-row">
            <div class="row-heading">
              Director: 
            </div>
          </div>
          <div class="info-card-row">
            <div class="director">
              {{ props.movie.director }}
            </div>
          </div>
          <div class="info-card-row">
            <div class="row-heading">
              Rental term: 
            </div>
          </div>
          <div class="info-card-row">
            <div class="rental-date">
              {{ rentalTerm }}
            </div>
          </div>
        </div>
      </div>
    </div>
    <h3 class="movie-title">{{ props.movie.title }}</h3>
    <div class="sessions-info">
      <div class="session-item" v-for="session in availableSessions" :key="session.startTime">
        <div class="session-content">
          {{ session.startTime }}
          <div class="price-card">{{ session.price }}$</div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.movie-item {
  background-color: #f0f0f0;
  padding: 10px;
  border-radius: 10px;
}

.session-content, .poster-wrapper {
  position: relative;
}

.movie-info-card {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(176, 176, 176, 0.2);
  color: black;
  font-size: large;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  padding: 20px;
  box-sizing: border-box;
  opacity: 0;
  transition: opacity 0.3s ease-in-out;
}

.poster-wrapper:hover .movie-info-card {
  opacity: 1;
}

.info-card-content {
  width: 80%;
  height: 90%;
}

.info-card-row {
  display: flex;
  flex-direction: row;
  margin-top: 10px;

  div {
    padding: 5px;
    border-radius: 10px;
    background-color: rgba(176, 176, 176, 1);
  }

  .age-restriction {
    margin-left: auto;
  }

  .row-heading {
    font-size: medium;
    color:rgb(37, 37, 37);
  }
}

.genre-grid {
  width: 100%;
  margin-top: 5px;
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(60px, 0.5fr));
  gap: 5px;

  div {
    padding: 5px;
    border-radius: 10px;
    background-color: rgba(176, 176, 176, 1);
    overflow:hidden;
  }
}

.poster-wrapper {
  overflow: hidden;
  transition: transform 0.3s ease-in-out;
  transition: all 0.5s ease;
}

.movie-poster {
  width: 100%;
  border-radius: 10px;
  transition: transform 0.3s ease-in-out;
}

.poster-wrapper:hover .movie-poster {
  transform: scale(1.1);
}

.movie-title {
  font-size: 18px;
  margin: 10px 0;
}

.sessions-info {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(50px, 1fr));
  gap: 5px;
}

.session-item {
  background-color: #eae7e7;
  padding: 5px;
  border-radius: 5px;
  text-align: center;
  transition: all 0.2s ease-in-out;
}

.session-item:hover {
  background-color: #ccc;
  cursor: pointer;
}

.price-card {
  position: absolute;
  top: 25px;
  left: 10px;
  transform: translateX(-50%);
  padding: 5px;
  background-color: #a0abc4;
  border: 1px solid #ccc;
  border-radius: 5px;
  font-size: 12px;
  visibility: hidden;
}

.session-item:hover .price-card {
  visibility: visible;
}
</style>