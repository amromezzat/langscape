import { FlashCardSetForm } from "./flashCardSetForm";
import { FlashCardWord, NewFlashCardWord } from "./flashCardWord";

export class FlashCardSetUpdateDto {
    id: string = '';
    name?: string = undefined;
    createdWords: NewFlashCardWord[] = [];
    updatedWords: FlashCardWord[] = [];
    deletedWords: string[] = [];

    constructor(setForm?: FlashCardSetForm) {
        if(setForm) {
          this.id = setForm.id!;
          this.name = setForm.name;
          setForm.words.forEach((word, index) => {
            word.updated ||= word.position !== index;
            word.position = index;
            
            if(word.isDeleted) {
                this.deletedWords.push(word.id!);
            } else if (!word.id) {
                this.createdWords.push(word as NewFlashCardWord);
            } else if (word.updated) {
                this.updatedWords.push(word as FlashCardWord);
            }
          });   
        }
    }
}