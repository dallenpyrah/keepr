<template>
  <div v-if="state.isLoading === false" class="container-fluid">
    <div class="row mt-5">
      <div v-if="state.profile.picture" class="col-xs-4 col-sm-4 col-md-4 col-lg-3 col-xl-2 text-center">
        <img class="img-fluid rounded image-size-profile animate__animated  animate__fadeIn" :src="state.profile.picture" />
      </div>
      <div class="col-2">
        <h3>{{ state.profile.name }}</h3>
        <h3><span class="badge badge-success">Vaults</span> {{ state.profileVaults.length }} </h3>
        <h3><span class="badge badge-dark">Keeps: </span> {{ state.profileKeeps.length }} </h3>
      </div>
    </div>
    <div class="row">
      <div class="col-12">
        <h2 class="mt-5 mb-4 ml-4">
          Vaults <i class="fa fa-plus  ml-3 text-secondary hover-icon" v-if="state.account.id == state.profile.id" data-target="#add-vault" data-toggle="modal" aria-hidden="true"></i>
        </h2>
        <add-vault-modal :profile-prop="state.profile" />
      </div>
    </div>
    <div class="card-columns vault-columns">
      <vault-component v-for="vault in state.profileVaults" :key="vault.id" :vault-prop="vault" />
    </div>
    <div class="row">
      <div class="col-12">
        <h2 class="mt-5 mb-5 ml-4">
          Keeps <i class="fa fa-plus  ml-3 text-secondary hover-icon" v-if="state.account.id == state.profile.id" data-target="#add-keep" data-toggle="modal" aria-hidden="true"></i>
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
import { useRoute } from 'vue-router'
import KeepComponent from '../components/KeepComponent.vue'
import AddKeepModal from '../components/AddKeepModal.vue'
import VaultComponent from '../components/VaultComponent.vue'
import AddVaultModal from '../components/AddVaultModal.vue'
import { accountService } from '../services/AccountService'
export default {
  components: { KeepComponent, AddKeepModal, VaultComponent, AddVaultModal },
  name: 'Account',
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
    onMounted(() => accountService.getAccountVaults(route.params.id))
    return {
      state
    }
  }
}
</script>

<style>

</style>
