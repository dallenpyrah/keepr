<template>
  <button @click="addKeepToVault" class="dropdown-item badge badge-success" href="#">
    {{ vaultProp.name }}  <i class="fa fa-lock" v-if="vaultProp.isPrivate === true" aria-hidden="true"></i>
  </button>
</template>

<script>
import { reactive } from 'vue'
import { vaultKeepsService } from '../services/VaultKeepsService'
import router from '../router'
import { keepsService } from '../services/KeepsService'
export default {
  name: 'VaultDropDown',
  props: {
    vaultProp: { type: Object, required: true },
    keepProp: { type: Object, required: true }
  },
  setup(props) {
    const state = reactive({

    })
    return {
      state,
      async addKeepToVault() {
        await vaultKeepsService.createVaultKeep({ vaultId: props.vaultProp.id, keepId: props.keepProp.id })
        await keepsService.editKeepCount(props.keepProp.id, props.keepProp)
        router.push({ name: 'VaultPage', params: { id: props.vaultProp.id } })
      }
    }
  }
}
</script>

<style>

</style>
