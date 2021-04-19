<template>
  <div class="AddVaultModal container-fluid">
    <div
      class="modal fade"
      id="add-vault"
      tabindex="-1"
      role="dialog"
      aria-labelledby="modelTitleId"
      aria-hidden="true"
    >
      <div class="modal-dialog  modal-dialog-centered modal-lg" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">
              New Vault
            </h5>
            <button
              type="button"
              class="close"
              data-dismiss="modal"
              aria-label="Close"
            >
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <form @submit.prevent="createVault">
              <div class="form-group">
                <h6>Title</h6>
                <input
                  type="text"
                  name="title"
                  class="form-control"
                  placeholder="Enter title..."
                  aria-describedby="helpId"
                  v-model="state.newVault.name"
                />
              </div>
              <div class="form-group">
                <h6>Img URL</h6>
                <input
                  type="text"
                  name="img"
                  class="form-control"
                  placeholder="Enter URL..."
                  aria-describedby="helpId"
                  v-model="state.newVault.img"
                />
              </div>
              <div class="form-group">
                <h6>Description</h6>
                <input
                  type="text"
                  name="description"
                  class="form-control input-lg"
                  placeholder="Enter Description..."
                  aria-describedby="helpId"
                  v-model="state.newVault.description"
                />
              </div>
              <div class="form-group">
                <input
                  type="checkbox"
                  name="isPrivate"
                  class=""
                  placeholder="Private?"
                  aria-describedby="helpId"
                  v-model="state.newVault.isPrivate"
                />
                Private?
              </div>
              <div class="modal-footer justify-content-end text-light">
                <button type="submit"
                        class="btn btn-success"
                >
                  Create
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive } from 'vue'
import { vaultsService } from '../services/VaultsService'
import $ from 'jquery'
export default {
  name: 'AddVaultModal',
  props: {
    profileProp: { type: Object, required: true }
  },
  setup(props) {
    const state = reactive({
      newVault: {}
    })
    return {
      state,
      async createVault() {
        await vaultsService.createVault(state.newVault)
        state.newVault = {}
        $('#add-vault').modal('hide')
      }
    }
  }

}
</script>

<style>

</style>
