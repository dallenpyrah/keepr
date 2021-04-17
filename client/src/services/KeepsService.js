import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class KeepsService {
  async getAllKeeps() {
    try {
      const res = await api.get('api/keeps')
      AppState.keeps = res.data
    } catch (error) {
      logger.log(error)
    }
  }

  async deleteKeep(id) {
    await api.delete('api/keeps/' + id)
    this.getAllKeeps()
  }
}

export const keepsService = new KeepsService()
