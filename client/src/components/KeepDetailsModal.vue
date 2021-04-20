<template>
  <div class="modal fade bd-example-modal-lg"
       :id="`view-keep` + keepProp.id"
       tabindex="-1"
       role="dialog"
       aria-labelledby="myLargeModalLabel"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered modal-xl">
      <div class="modal-content text-dark">
        <div class="row">
          <div class="col-5 animate__animated animate__fadeIn">
            <img class="img-fluid m-4 rounded" :src="keepProp.img">
          </div>
          <div class="col-7 align-items-end text-center">
            <div class="row mt-4">
              <div class="col-11 text-right">
                <i class="fa fa-times icon-size close" data-dismiss="modal" aria-label="Close"></i>
              </div>
            </div>
            <div class="row justify-content-center">
              <div class="col-12 text-center margin-top-numbers">
                <div>
                  <i class="fa fa-eye icon-size text-success margin-right-icon" aria-hidden="true"></i><span class="number-size margin-left-data">{{ keepProp.views }}</span>
                  <i class="fa fa-vk icon-size text-success margin-right-icon margin-left-icon" aria-hidden="true"></i><span class="number-size margin-left-data">{{ keepProp.keeps }}</span>
                  <i class="fa fa-share-alt icon-size-share text-success margin-left-icon margin-right-icon" aria-hidden="true"></i><span class="number-size margin-left-data">{{ keepProp.shares }}</span>
                </div>
              </div>
            </div>
            <div class="row justify-content-center margin-top-name">
              <div class="col-12">
                <h1 class="text-size-name">
                  {{ keepProp.name }}
                </h1>
              </div>
            </div>
            <div class="row justify-content-center margin-top-description">
              <div class="col-10">
                <p class="text-size-description">
                  {{ keepProp.description }}
                </p>
              </div>
            </div>
            <div class="row justify-content-center margin-top-description">
              <div class="col-10">
                <span class="text-size-description badge badge-success">
                  {{ keepProp.tags }}
                </span>
              </div>
            </div>
            <div class="row justify-content-center">
              <div class="col-11 height-row">
                <div class="row justify-content-center">
                  <div class="col-xs-2 col-sm-6 col-md-6 col-lg-4 col-xl-4" v-if="state.user.isAuthenticated">
                    <div class="dropup">
                      <button class="btn btn-success btn-sm dropdown-toggle animate__animated  animate__bounceIn dropright dropdown-text"
                              type="button"
                              id="triggerId"
                              data-toggle="dropdown"
                              aria-haspopup="true"
                              aria-expanded="false"
                      >
                        ADD TO VAULT
                      </button>
                      <div class="dropdown-menu view-height-dropdown" aria-labelledby="triggerId">
                        <vault-drop-down data-dismiss="modal"
                                         aria-label="Close"
                                         v-for="vault in state.profileVaults"
                                         :key="vault.id"
                                         :vault-prop="vault"
                                         :keep-prop="keepProp"
                        />
                      </div>
                    </div>
                  </div>
                  <div class="col-xs-2 col-sm-6 col-md-6 col-lg-4 col-xl-4" v-if="keepProp.creator && state.user.email == keepProp.creator.email">
                    <i v-if="state.vaultPage === false"
                       class="fa fa-trash modal-delete hover-icon-delete"
                       data-dismiss="modal"
                       aria-label="Close"
                       @click="deleteKeep"
                       aria-hidden="true"
                    ></i>
                    <i v-if="state.vaultPage === true"
                       class="fa fa-trash modal-delete text-warning hover-icon-delete"
                       data-dismiss="modal"
                       aria-label="Close"
                       @click="deleteVaultKeep"
                       aria-hidden="true"
                    ></i>
                  </div>
                  <div class="col-4" v-else>
                  </div>
                  <div v-if="state.user.isAuthenticated" class="col-xs-12 col-sm-12 col-md-12 col-lg-4 col-xl-4">
                    <div v-if="keepProp.creator" class="card-img-top">
                      <img class="img-fluid width-image keep rounded-image"
                           data-dismiss="modal"
                           aria-label="Close"
                           @click="toProfilePage"
                           :src="keepProp.creator.picture"
                      >
                      <span class="ml-3 animate__animated animate__fadeInRight">{{ keepProp.creator.name }}</span>
                    </div>
                  </div>
                  <div v-else class="ml-5 col-xs-12 col-sm-12 col-md-12 col-lg-4 col-xl-4">
                    <div class="card-img-top">
                      <img class="img-fluid width-image keep rounded-image"
                           data-dismiss="modal"
                           aria-label="Close"
                           @click="toProfilePage"
                           :src="keepProp.creator.picture"
                      >
                      <span class="ml-3 animate__animated animate__fadeInRight">{{ keepProp.creator.name }}</span>
                    </div>
                  </div>
                </div>
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
import { AppState } from '../AppState'
import VaultDropDown from './VaultDropDown.vue'
import { keepsService } from '../services/KeepsService'
import Swal from 'sweetalert2'
import router from '../router'
import { vaultKeepsService } from '../services/VaultKeepsService'
import 'animate.css'
export default {
  components: { VaultDropDown },
  name: 'KeepDetailsModal',
  props: {
    keepProp: { type: Object, required: true },
    vaultProp: { type: Object }
  },
  setup(props) {
    const state = reactive({
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      activeKeep: computed(() => AppState.activeKeep),
      profileVaults: computed(() => AppState.profileVaults),
      vaultPage: computed(() => AppState.vaultPage)
    })
    return {
      state,
      async deleteKeep() {
        Swal.fire({
          title: 'Are you sure you want to delete this keep?',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
          if (result.isConfirmed) {
            keepsService.deleteKeep(props.keepProp.id)
            Swal.fire(
              'Deleted!',
              'Your keep has been deleted.',
              'success'
            )
          }
        })
      },
      async deleteVaultKeep() {
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
      },
      toProfilePage() {
        router.push({ name: 'ProfilePage', params: { id: props.keepProp.creatorId } })
      }
    }
  }

}
</script>

<style scoped>
.rounded{
  border-radius: .75rem!important;
}
.rounded-image{
  border-radius: .5rem!important;
}
.keep:hover{
  transform: translateY(-10px);
  cursor: pointer;
}
.keep{
  transition: all .3s;
}
.dropdown-menu {
 overflow-y:auto;
 max-height:80vh
}
.hover-icon-delete:hover{
  color: #ff4141!important;
  font-size: 40px;
}
.hover-icon-delete{
  transition: all .25s;
}
</style>
