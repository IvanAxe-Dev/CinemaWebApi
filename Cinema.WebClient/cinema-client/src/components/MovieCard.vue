<script setup>
import { defineProps, computed } from 'vue';

const props = defineProps({
  movie: {
    type: Object,
    required: true
  }
});

const placeholderPoster = "https://via.placeholder.com/300";
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
    <img :src="imageUrl" alt="Movie Poster" class="movie-poster">
    <h3 class="movie-title">{{ props.movie.title }}</h3>
    <div class="sessions-info">
      <div class="session-item" v-for="session in availableSessions" :key="session.startTime">
        {{ session.startTime }}
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

.movie-poster {
  width: 100%;
  border-radius: 10px;
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
</style>