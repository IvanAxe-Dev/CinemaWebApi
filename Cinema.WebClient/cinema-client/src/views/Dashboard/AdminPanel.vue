<script>
import UpdateView from './Movies/UpdateView.vue';
import UploadView from './Movies/UploadView.vue';

export default {
  components: {
    UpdateView,
    UploadView
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
      this.currentView = 'UpdateView'
    }
  }
}
</script>

<template>
  <div class="admin-panel">
    <div class="panel-navigation">
      <button class="nav-button" @click="goToUpload">Upload movie</button>
      <button class="nav-button" @click="goToUpdate">Update movie</button>
    </div>
    <div class="panel-conent">
      <UploadView v-if="currentView === 'UploadView'"></UploadView>
      <UpdateView v-if="currentView === 'UpdateView'"></UpdateView>
    </div>
  </div>
</template>