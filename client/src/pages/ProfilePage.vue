<template>
  <div v-if="state.isLoading === false" class="container-fluid">
    <div class="row mt-4">
      <div class="col-4">
        <img class="img-fluid rounded" width="175" :src="state.account.picture" />
      </div>
      <div class="col-4">
        <h4>{{ state.account.name }}</h4>
        <h6>Vaults: {{ state.profileVaults.length }} </h6>
        <h6>Keeps: {{ state.profileKeeps.length }} </h6>
      </div>
    </div>
    <div class="row">
      <div class="col-12">
        <h2 class="mt-5 mb-2">
          Vaults <i class="fa fa-plus text-success" v-if="state.account.id == state.profile.id" data-target="#add-vault" data-toggle="modal" aria-hidden="true"></i>
        </h2>
        <add-vault-modal :profile-prop="state.profile" />
      </div>
    </div>
    <div class="card-columns vault-columns">
      <vault-component v-for="vault in state.profileVaults" :key="vault.id" :vault-prop="vault" />
    </div>
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
  <div v-else class="container-fluid">
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { profilesService } from '../services/ProfilesService'
import { onBeforeRouteLeave, useRoute } from 'vue-router'
import KeepComponent from '../components/KeepComponent.vue'
import AddKeepModal from '../components/AddKeepModal.vue'
import VaultComponent from '../components/VaultComponent.vue'
import AddVaultModal from '../components/AddVaultModal.vue'
import { accountService } from '../services/AccountService'
export default {
  components: { KeepComponent, AddKeepModal, VaultComponent, AddVaultModal },
  name: 'ProfilePage',
  setup() {
    const route = useRoute()
    const state = reactive({
      account: computed(() => AppState.account),
      user: computed(() => AppState.user),
      profile: computed(() => AppState.profile),
      profileKeeps: computed(() => AppState.profileKeeps),
      profileVaults: computed(() => AppState.profileVaults),
      isLoading: computed(() => AppState.isLoading)
    })
    onMounted(async() => await profilesService.GetUserProfile(route.params.id))
    onMounted(async() => await profilesService.getUserKeeps(route.params.id))
    if (state.account.id !== route.params.id) {
      onMounted(() => profilesService.getUserVaults(route.params.id))
    }
    if (state.account.id === route.params.id) {
      onMounted(() => accountService.getAccountVaults(route.params.id))
    }
    onBeforeRouteLeave((to, from, next) => {
      AppState.profileKeeps = []
      AppState.profileVaults = []
      route.params.id = null
      next()
    })
    return {
      state
    }
  }
}
</script>

<style>

</style>
