<template>
  <div class="AddKeepModal container-fluid">
    <div
      class="modal fade"
      id="add-keep"
      tabindex="-1"
      role="dialog"
      aria-labelledby="modelTitleId"
      aria-hidden="true"
    >
      <div class="modal-dialog  modal-dialog-centered modal-lg" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">
              New Keep
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
            <form @submit.prevent="createKeep">
              <div class="form-group">
                <h6>Title</h6>
                <input
                  type="text"
                  name="title"
                  class="form-control"
                  placeholder="Enter title..."
                  aria-describedby="helpId"
                  v-model="state.newKeep.name"
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
                  v-model="state.newKeep.img"
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
                  v-model="state.newKeep.description"
                />
              </div>
              <div class="form-group">
                <h6>Tags</h6>
                <input
                  type="text"
                  name="tags"
                  class="form-control"
                  placeholder="Enter tags..."
                  aria-describedby="helpId"
                  v-model="state.newKeep.tags"
                />
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
import { reactive, computed } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import $ from 'jquery'
import { profilesService } from '../services/ProfilesService'
export default {
  name: 'AddKeepModal',
  setup() {
    const state = reactive({
      account: computed(() => AppState.account),
      user: computed(() => AppState.user),
      newKeep: {}
    })
    return {
      state,
      async createKeep() {
        console.log(state.newKeep)
        state.newKeep.tags.split(',')
        await keepsService.createKeep(state.newKeep)
        await profilesService.getUserKeeps(state.account.id)
        state.newKeep = {}
        $('#add-keep').modal('hide')
      }
    }
  },
  components: {}
}
</script>

<style scoped>
</style>
