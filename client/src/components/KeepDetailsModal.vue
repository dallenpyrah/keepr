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
          <div class="col-6">
            <img class="img-fluid m-4 rounded" :src="keepProp.img">
          </div>
          <div class="col-6 text-center">
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
            <div class="row">
              <div class="col-12">
                <div class="row justify-content-around">
                  <div v-if="state.user.isAuthenticated">
                    <div class="dropdown">
                      <button class="btn btn-success dropdown-toggle"
                              type="button"
                              id="triggerId"
                              data-toggle="dropdown"
                              aria-haspopup="true"
                              aria-expanded="false"
                      >
                        ADD TO VAULT
                      </button>
                      <div class="dropdown-menu" aria-labelledby="triggerId">
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
                  <div class="col-4" v-if="keepProp.creator && state.user.email == keepProp.creator.email">
                    <i class="fa fa-trash icon-size"
                       data-dismiss="modal"
                       aria-label="Close"
                       @click="deleteKeep"
                       aria-hidden="true"
                    ></i>
                  </div>
                  <div class="col-4" v-else>
                  </div>
                  <div class="col-4">
                    <div class="card-img-top">
                      <img class="img-fluid keep w-25 rounded"
                           data-dismiss="modal"
                           aria-label="Close"
                           @click="toProfilePage"
                           :src="keepProp.creator.picture"
                      >
                      {{ keepProp.name }}
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
export default {
  components: { VaultDropDown },
  name: 'KeepDetailsModal',
  props: {
    keepProp: { type: Object, required: true }
  },
  setup(props) {
    const state = reactive({
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      activeKeep: computed(() => AppState.activeKeep),
      profileVaults: computed(() => AppState.profileVaults)
    })
    return {
      state,
      async deleteKeep() {
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
            keepsService.deleteKeep(props.keepProp.id)
            Swal.fire(
              'Deleted!',
              'Your file has been deleted.',
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

<style>
.rounded{
  border-radius: .75rem!important;
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
</style>
