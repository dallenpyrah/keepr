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
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
export default {
  name: 'KeepDetailsModal',
  props: {
    keepProp: { type: Object, required: true }
  },
  setup() {
    const state = reactive({
      activeKeep: computed(() => AppState.activeKeep)
    })
    return {
      state
    }
  }

}
</script>

<style>
.rounded{
  border-radius: .75rem!important;
}
</style>
