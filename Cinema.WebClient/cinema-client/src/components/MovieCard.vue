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
</script>

<template>
  <div class="movie-item">
    <div class="poster-wrapper">
      <img :src="imageUrl" alt="Movie Poster" class="movie-poster">
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

.poster-wrapper {
  position: relative;
  overflow: hidden;
  transition: transform 0.3s ease-in-out;
  transition: all 0.5s ease;
}

.movie-poster {
  width: 100%;
  border-radius: 10px;
  transition: transform 0.3s ease-in-out;
}

.movie-poster:hover {
  transform: scale(1.1);
}

.movie-title {
  font-size: 18px;
  margin: 10px 0;
}

.movie-description {
  font-size: 14px;
}

.additional-info {
  padding: 10px 0;
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
}

.session-content {
  position: relative;
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