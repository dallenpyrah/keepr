<template>
  <keep-details-modal v-if="vaultProp" :keep-prop="keepProp" :vault-prop="vaultProp" />
  <keep-details-modal v-else :keep-prop="keepProp" />
  <div class="container-fluid">
    <div class="card-container keep" v-if="state.user.isAuthenticated" @click="getUserVaults">
      <div class="card rounded">
        <img
          class="card-img rounded"
          :src="keepProp.img"
          alt="Card image"
        >
        <div class="card-img-overlay d-flex align-items-end">
          <div class="card-img-overlay"
               :data-target="`#view-keep` + keepProp.id"
               data-toggle="modal"
               aria-hidden="true"
          >
          </div>
          <div class="row w-100 justify-content-between">
            <div class="col-12 text-light">
              <div class="row justify-content-between">
                <h6 class="keep-name text-left ml-3">
                  {{ keepProp.name }}
                </h6>
                <i class="fa fa-user icon-size text-right text-light hover-icon" v-if="route.name == 'Home'" aria-hidden="true" @click="toProfilePage"></i>
                <i class="fa fa-times-circle-o icon-size text-right text-light danger hover-icon" v-if="route.name == 'VaultPage' " @click="deleteKeep" aria-hidden="true"></i>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="card-container keep" v-else>
      <div class="card rounded">
        <img
          class="card-img rounded"
          :src="keepProp.img"
          alt="Card image"
        >
        <div class="card-img-overlay d-flex align-items-end">
          <div class="card-img-overlay"
               :data-target="`#view-keep` + keepProp.id"
               data-toggle="modal"
               aria-hidden="true"
          >
          </div>
          <div class="row w-100 justify-content-between">
            <div class="col-12 text-light">
              <div class="row justify-content-between">
                <h6 class="keep-name text-left ml-3">
                  {{ keepProp.name }}
                </h6>
                <i class="fa fa-user icon-size text-right text-light hover-icon" aria-hidden="true" @click="toProfilePage"></i>
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
        accountService.getAccount()
        accountService.getAccountVaults(state.account.id)
      },
      deleteKeep() {
        keepsService.deleteKeep(props.keepProp.id, props.vaultProp.id)
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
</style>
