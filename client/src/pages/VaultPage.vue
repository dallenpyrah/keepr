<template>
  <div class="container-fluid">
    <div class="row justify-content-center mt-4">
      <div class="col-11 ml-5 mt-4 mb-5">
        <h1>{{ state.vault.name }}  <i class="fa fa-trash text-danger ml-3" v-if="state.vault.creator && state.user.email == state.vault.creator.email" @click="deleteVault" aria-hidden="true"></i></h1>
        <h4 class="mt-4">
          <span class="badge badge-success">Keeps {{ state.keeps.length }}</span>
        </h4>
      </div>
      <div class="col-11">
        <div class="card-columns">
          <keep-component v-for="keep in state.keeps" :key="keep.id" :keep-prop="keep" :vault-prop="state.vault" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { vaultKeepsService } from '../services/VaultKeepsService'
import { onBeforeRouteLeave, useRoute } from 'vue-router'
import { AppState } from '../AppState'
import KeepComponent from '../components/KeepComponent.vue'
import { vaultsService } from '../services/VaultsService'
import Swal from 'sweetalert2'
import router from '../router'
export default {
  components: { KeepComponent },
  name: 'VaultPage',
  setup() {
    const route = useRoute()
    const state = reactive({
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.vaultKeeps),
      vault: computed(() => AppState.activeVault)
    })
    onMounted(() => vaultKeepsService.getKeepsByVaultId(route.params.id))
    onMounted(() => vaultsService.getVaultById(route.params.id))
    onBeforeRouteLeave((to, from, next) => {
      AppState.vaultPage = false
      next()
    })
    return {
      state,
      route,
      deleteVault() {
        Swal.fire({
          title: 'Are you sure?',
          text: "You won't be able to revert this!",
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
          if (result.isConfirmed) {
            vaultsService.deleteVault(state.vault.id)
            Swal.fire(
              'Deleted!',
              'Your file has been deleted.',
              'success'
            )
            router.push({ name: 'ProfilePage', params: { id: state.account.id } })
          }
        })
      }
    }
  }
}
</script>

<style>

</style>
