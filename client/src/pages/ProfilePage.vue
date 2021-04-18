<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12">
        <h2 class="mt-5 mb-2">
          Keeps <i class="fa fa-plus text-success" v-if="state.account.id == state.profile.id" data-target="#add-keep" data-toggle="modal" aria-hidden="true"></i>
          <add-keep-modal />
        </h2>
      </div>
    </div>
    <div class="card-columns">
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
import AddKeepModal from '../components/AddKeepModal.vue'
export default {
  components: { KeepComponent, AddKeepModal },
  name: 'ProfilePage',
  setup() {
    const route = useRoute()
    const state = reactive({
      account: computed(() => AppState.account),
      user: computed(() => AppState.user),
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
