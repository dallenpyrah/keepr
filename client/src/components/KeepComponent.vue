<template>
  <keep-details-modal :keep-prop="keepProp" />
  <div class="card-container keep p-2">
    <div class="card rounded">
      <img
        class="card-img rounded"
        :src="keepProp.img"
        alt="Card image"
      >
      <div class="card-img-overlay d-flex align-items-end mt-5">
        <div class="card-img-overlay"
             :data-target="`#view-keep` + keepProp.id"
             data-toggle="modal"
             aria-hidden="true"
        >
        </div>
        <div class="row w-100 justify-content-beteween">
          <div class="col-7 text-left text-light ">
            <h6 class="card-title keep-name">
              {{ keepProp.name }}
            </h6>
          </div>
          <div class="col-5 text-right">
            <i class="fa fa-user fa-2x keep-height text-light" aria-hidden="true" @click="toProfilePage"></i>
          </div>
        </div>
      </div>
    </div>
  </div>
  <!-- <div class="col-3 text-white">
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
  </div> -->
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
.rounded{
  border-radius: .75rem!important;
}
.keep:hover{
  transform: translateY(-10px);
}
.keep{
  transition: all .3s;
}
</style>
