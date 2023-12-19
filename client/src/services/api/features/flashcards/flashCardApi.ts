import { FlashCardSet } from "../../../../models/flashCards/flashCardSet";
import { FlashCardSetDto } from "../../../../models/flashCards/flashCardSetDto";
import { FlashCardSetForm } from "../../../../models/flashCards/flashCardSetForm";
import { FlashCardWord } from "../../../../models/flashCards/flashCardWord";
import { apiRequest } from "../../core/apiRequest";

export const flashCardApi = {
    getCardSets: (filters?: Map<string, any>) => apiRequest.get<FlashCardSet[]>('/flashcards', filters),
    getCardSet: (setId: string) => apiRequest.get<FlashCardSet>(`/flashcards/${setId}`),
    addFavoriteSet: (setId: string) => apiRequest.post(`/flashCards/favorites/${setId}`, {}),
    removeFavoriteSet: (setId: string) => apiRequest.del(`/flashCards/favorites/${setId}`),
    updateWord: (setId: string, word: FlashCardWord) => apiRequest.put(`/flashCards/${setId}`, {'updatedWords': [word]}),
    updateSet: (setDto: FlashCardSetDto) => apiRequest.put(`/flashCards/${setDto.id}`, setDto),
    createSet: (set: FlashCardSetForm) => apiRequest.put<FlashCardSet>(`/flashCard/`, set)
}