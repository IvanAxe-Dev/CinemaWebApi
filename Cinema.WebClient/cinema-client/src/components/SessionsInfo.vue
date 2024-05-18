<script setup>
import { defineProps, computed } from 'vue';
import { formatSessionsData } from '@/utils';

const props = defineProps({
  movie: {
    type: Object,
    required: true
  }
});

const sessionsGroups = computed(() => {
  const currentSessions = props.movie.sessions.filter(session => session.availableSeats > 0)
  return formatSessionsData(currentSessions);
});

const sessionsAvailable = computed(() => {
  return sessionsGroups.value.length > 0;
});

</script>

<template>
  <div class="sessions-info">
    <h1>Available sessions</h1>
    <div class="session-content" v-if="sessionsAvailable">
      <div v-for="(sessionGroup, i) in sessionsGroups" :key="i">
        <span class="date-of-session">{{ sessionGroup.date }}</span>
        <div v-for="(sessionData, j) in sessionGroup.sessions" :key="j">
          <div class="session-tile">
            <span class="session-data">{{ sessionData.startTime }}</span>
            <span class="session-data">Seats: {{ sessionData.availableSeats }}</span>
            <span class="session-data">Price: {{ sessionData.price }}</span>
          </div>
        </div>
      </div>
    </div>
    <div class="no-sessions" v-if="!sessionsAvailable">
      No available sessions
    </div>
  </div>
</template>

<style scoped lang="scss">
.sessions-info {
  text-align: left;

  h1 {
    color: #393952;
  }

  div {
    div {
      padding: 10px;
    }
  }
}

.sessions-content {
  max-width: 400px;
  display: flex;
  justify-content: space-between;
}

.date-of-session {
  font-size: larger;
}

.session-tile {
  border-radius: 15px;
  background-color: #bebede;
}

.date-of-session, .session-data {
  padding: 10px;
}

.session-data {
  font-weight:400;
}

.no-sessions {
  color: rgb(95, 95, 95)}
</style>