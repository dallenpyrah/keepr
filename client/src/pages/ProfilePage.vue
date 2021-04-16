<template>
  <div class="container-fluid">
    <h1>{{ state.profile.name }}</h1>
    <div class="row">
      <keep-component v-for="keep in state.profileKeeps" :key="keep.id" :keep-prop="keep" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { profilesService } from '../services/ProfilesService'
import { useRoute } from 'vue-router'
import KeepComponent from '../components/KeepComponent.vue'
export default {
  components: { KeepComponent },
  name: 'ProfilePage',
  setup() {
    const route = useRoute()
    const state = reactive({
      profile: computed(() => AppState.profile),
      profileKeeps: computed(() => AppState.profileKeeps)
    })
    onMounted(() => profilesService.GetUserProfile(route.params.id))
    onMounted(() => profilesService.getUserKeeps(route.params.id))
    return {
      state
    }
  }
}
</script>

<style>

</style>
