<template>
  <div class="card-container" @click="toVaultPage">
    <div class="card rounded keep">
      <img
        class="card-img rounded"
        :src="vaultProp.img"
        alt="Card image"
      >
      <div class="card-img-overlay d-flex align-items-end">
        <div class="row w-100 justify-content-between">
          <div class="col-12 text-light">
            <div class="row justify-content-between">
              <h6 class="keep-name text-light text-left ml-3">
                {{ vaultProp.name }}
              </h6>
              <i class="fa fa-lock mt-1" v-if="vaultProp.isPrivate" aria-hidden="true"></i>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive } from 'vue'
import { useRouter } from 'vue-router'
export default {
  name: 'VaultComponenet',
  props: {
    vaultProp: { type: Object, required: true }
  },
  setup(props) {
    const router = useRouter()
    const state = reactive({
    })
    return {
      state,
      toVaultPage() {
        router.push({
          name: 'VaultPage',
          params: { id: props.vaultProp.id }
        })
      }
    }
  }

}
</script>

<style>

</style>
