<template>
  <div class="modal fade bd-example-modal-lg"
       :id="`view-keep` + keepProp.id"
       tabindex="-1"
       role="dialog"
       aria-labelledby="myLargeModalLabel"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered modal-lg">
      <div class="modal-content text-dark">
        <h1>{{ keepProp.name }}</h1>
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

</style>
