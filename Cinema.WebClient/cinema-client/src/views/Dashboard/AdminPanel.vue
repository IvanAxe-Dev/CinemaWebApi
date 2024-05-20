<script>
import UploadView from './Movies/UploadView.vue';
import GetMovie from './Movies/GetView.vue';

export default {
  components: {
    UploadView,
    GetMovie
  },
  data() {
    return {
      currentView: 'GetView'
    }
  },
  methods: {
    goToUpload() {
      this.currentView = 'UploadView'
    },
    goToUpdate() {
      this.currentView = 'GetView'
    }
  }
}
</script>

<template>
  <div class="admin-panel">
    <div class="panel-navigation">
      <button class="nav-button" @click="goToUpdate">Get movies</button>
      <button class="nav-button" @click="goToUpload">Upload movie</button>
    </div>
    <div class="panel-content">
      <GetMovie v-if="currentView === 'GetView'"></GetMovie>
      <UploadView v-if="currentView === 'UploadView'"></UploadView>
    </div>
  </div>
</template>

<style scoped lang="scss">
.admin-panel {
  display: flex;
}

.panel-navigation {
  display: flex;
  flex-direction: column;
  margin-right: 20px;

  button {
    font-size: small;
    font-weight: bold;
    padding: 10px;
    border-radius: 15px;
    border: none;
    color: white;
    background-color: #4a4a67;
    transition: all 0.5s ease;
  }

  button:hover {
    background-color: #8b8bc2;
    opacity: 0.8;
  }
}

.panel-navigation {
  margin-left: 15px;
}

.panel-navigation .nav-button {
  margin-bottom: 10px;
}

.panel-content {
  flex-grow: 1;
}

@media (max-width: 796px) {
  .admin-panel{
    flex-direction: column;
  }

  .panel-navigation {
    display: flex;
    flex-direction: row;
    align-items: center;
    margin-bottom: 5px;

    button {
      margin-right: 20px;
    }
  }
}
</style>