<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12">
        {{ state.vault.name }}
        Keeps: {{ state.keeps.length }}
        <i class="fa fa-trash" v-if="state.vault.creator && state.user.email == state.vault.creator.email" @click="deleteVault" aria-hidden="true"></i>
      </div>
    </div>
    <div class="card-columns">
      <keep-component v-for="keep in state.keeps" :key="keep.id" :keep-prop="keep" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { vaultKeepsService } from '../services/VaultKeepsService'
import { useRoute } from 'vue-router'
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
            router.push({ name: 'Home' })
          }
        })
      }
    }
  }
}
</script>

<style>

</style>
