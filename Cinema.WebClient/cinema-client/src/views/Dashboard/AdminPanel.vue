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
      currentView: ''
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
      <button class="nav-button" @click="goToUpload">Upload movie</button>
      <button class="nav-button" @click="goToUpdate">Get movies</button>
    </div>
    <div class="panel-conent">
      <UploadView v-if="currentView === 'UploadView'"></UploadView>
      <GetMovie v-if="currentView === 'GetView'"></GetMovie>
    </div>
  </div>
</template>