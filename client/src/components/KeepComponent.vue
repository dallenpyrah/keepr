<template>
  <div class="col-3 text-white">
    <img
      class="card-img"
      :src="keepProp.img"
      alt="Card image"
    >
    <div class="card-img-overlay"
         :data-target="`#view-keep` + keepProp.id"
         data-toggle="modal"
         aria-hidden="true"
    >
      <div class="row justify-content-around mt-keep">
        <div class="col-5 text-left">
          <keep-details-modal :keep-prop="keepProp" />
          <h5 class="ml-2 card-title">
            {{ keepProp.name }}
          </h5>
        </div>
        <div class="col-5 mr-1 text-right">
          <i class="fa fa-user" aria-hidden="true" @click="toProfilePage"></i>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import KeepDetailsModal from './KeepDetailsModal.vue'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'
export default {
  components: { KeepDetailsModal },
  name: 'KeepComponent',
  props: {
    keepProp: { type: Object, required: true }
  },
  setup(props) {
    const router = useRouter()
    const state = reactive({
      user: computed(() => AppState.user)
    })
    return {
      state,
      toProfilePage() {
        router.push({ name: 'ProfilePage', params: { id: props.keepProp.creatorId } })
      }
    }
  }
}
</script>

<style>

</style>
