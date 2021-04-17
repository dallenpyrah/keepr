<template>
  <button @click="addKeepToVault" class="dropdown-item" href="#">
    {{ vaultProp.name }}
  </button>
</template>

<script>
import { reactive } from 'vue'
import { vaultKeepsService } from '../services/VaultKeepsService'
import router from '../router'
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
        router.push({ name: 'VaultPage', params: { id: props.vaultProp.id } })
      }
    }
  }
}
</script>

<style>

</style>
