<template>
  <keep-details-modal v-if="vaultProp" :keep-prop="keepProp" :vault-prop="vaultProp" />
  <keep-details-modal v-else :keep-prop="keepProp" />
  <div v-if="state.keeps != []" class="container-fluid">
    <div class="card-container keep" @click="getUserVaults">
      <div class="card rounded">
        <img
          class="card-img rounded"
          :src="keepProp.img"
          alt="Card image"
        >
        <div @click="increaseViewCount" class="card-img-overlay d-flex align-items-end">
          <div
            class="card-img-overlay"
            :data-target="`#view-keep` + keepProp.id"
            data-toggle="modal"
            aria-hidden="true"
          >
          </div>
          <div class="row w-100 justify-content-between">
            <div class="col-12 text-light">
              <div class="row justify-content-between">
                <h6 class="keep-name animate__animated  animate__fadeInLeft text-left ml-3">
                  {{ keepProp.name }}
                </h6>
                <i class="fa fa-user icon-size text-right text-light animate__animated  animate__fadeInRight hover-icon" v-if="route.name == 'Home'" aria-hidden="true" @click="toProfilePage"></i>
                <i class="fa fa-times-circle-o icon-size ml-icon text-right text-light animate__animated  animate__fadeInRight danger hover-icon-delete" v-if="route.name == 'VaultPage' && keepProp.creator && state.user.email == keepProp.creator.email" @click="deleteKeep" aria-hidden="true"></i>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import KeepDetailsModal from './KeepDetailsModal.vue'
import { useRoute, useRouter } from 'vue-router'
import { AppState } from '../AppState'
import { accountService } from '../services/AccountService'
import { keepsService } from '../services/KeepsService'
import { vaultKeepsService } from '../services/VaultKeepsService'
import Swal from 'sweetalert2'
export default {
  components: { KeepDetailsModal },
  name: 'KeepComponent',
  props: {
    keepProp: { type: Object, required: true },
    vaultProp: { type: Object }
  },
  setup(props) {
    const route = useRoute()
    const router = useRouter()
    const state = reactive({
      account: computed(() => AppState.account),
      user: computed(() => AppState.user),
      homepage: computed(() => AppState.homepage),
      vaultpage: computed(() => AppState.vaultpage)
    })
    return {
      state,
      route,
      toProfilePage() {
        router.push({ name: 'ProfilePage', params: { id: props.keepProp.creatorId } })
      },
      async getUserVaults() {
        if (state.user.isAuthenticated) {
          accountService.getAccount()
          accountService.getAccountVaults(state.account.id)
        }
      },
      async increaseViewCount() {
        if (state.user.isAuthenticated) {
          await keepsService.editViewCount(props.keepProp.id, props.keepProp)
        }
      },
      deleteKeep() {
        Swal.fire({
          title: 'Are you sure you want to remove this keep from your vault?',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Yes, remove it!'
        }).then((result) => {
          if (result.isConfirmed) {
            vaultKeepsService.deleteVaultKeep(props.keepProp.vaultKeepId, props.vaultProp.id)
            Swal.fire(
              'Removed!',
              'Your keep has been removed.',
              'success'
            )
          }
        })
      }
    }
  }
}
</script>

<style>
.rounded{
  border-radius: .75rem!important;
}
.keep:hover{
  transform: translateY(-10px);
  cursor: pointer;
}
.keep{
  transition: all .25s;
}
.danger:hover{
  color: rgb(255, 80, 80);
  cursor: pointer;
}
.danger{
  transition: all .25s;
}
.hover-icon:hover{
  color: #41ffb6!important;
  font-size: 30px;
}
.hover-icon{
  transition: all .25s;
}
.hover-icon-delete:hover{
  color: #ff4141!important;
  font-size: 30px;
}
.hover-icon-delete{
  transition: all .25s;
}
</style>
