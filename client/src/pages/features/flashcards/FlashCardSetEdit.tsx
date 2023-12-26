import { Formik } from "formik"
import { useEffect, useState } from "react";
import { Button, Form, Loader, Segment } from "semantic-ui-react"
import * as Yup from 'yup';
import { FlashCardSetForm } from "../../../models/flashCards/flashCardSetForm";
import ValidatableTextInput from "../../../components/common/form/ValidatableTextInput";
import { Link, useNavigate, useParams } from "react-router-dom";
import { useStore } from "../../../stores/core/store";
import { FlashCardSetUpdateDto } from "../../../models/flashCards/flashCardSetUpdateDto";
import SetFormEditList from "../../../components/features/flashCards/setForm/SetFormEditList";
import { observer } from "mobx-react-lite";
import { FlashCardSetCreateDto } from "../../../models/flashCards/flashCardSetCreateDto";
import '../../../styles/Common.css'

export default observer (function FlashCardSetEdit() {

    const {id} = useParams();
    const [wordsForm, setWordsForm] = useState<FlashCardSetForm>(new FlashCardSetForm());
    const {flashCardStore:{ isLoading, loadSet, updateSet, createSet, clearFilter }} = useStore();
    const navigate = useNavigate();

    const validationSchema = Yup.object({
        name: Yup.string().required('The set name can\'t be empty'),
        words: Yup.array()
            .required()
            .min(1, 'Set must contain at least one word')
            .of(Yup.object().shape({
                word: Yup.string().required('The word can\'t be empty'),
                translation: Yup.string().required('The translation can\'t be empty')
            }))
    })

    useEffect(() => {
        clearFilter();
        if(id) {
            loadSet(id).then(set => setWordsForm(new FlashCardSetForm(set)));
        }
    }, [id, loadSet, clearFilter])

    function handleFormSubmit(setForm: FlashCardSetForm) {
        if(!setForm.id) {
            createSet(new FlashCardSetCreateDto(setForm)).then(id => navigate(`/sets/${id}`))
        } else {
            updateSet(new FlashCardSetUpdateDto(setForm)).then(() => navigate(`/sets/${setForm.id}`))
        }
    }

    return (
        <Segment clearing basic>
            {isLoading ?
                <Loader active inline /> : 
                <Formik 
                    validationSchema={validationSchema} 
                    enableReinitialize 
                    initialValues={wordsForm} 
                    onSubmit={values => handleFormSubmit(values)}
                >
                    {({ isValid, isSubmitting, dirty, values, handleSubmit, setFieldValue }) => (
                        <Form className='ui form' onSubmit={handleSubmit} autoComplete='off'>
                            <Segment basic clearing className='no-padding'>
                                {id && <Button 
                                    floated='left'
                                    content='Back to set'
                                    icon='left arrow'
                                    className='no-padding-left borderless-button white-text'
                                    type='button'
                                    as={Link}
                                    to={`/sets/${wordsForm.id}`}
                                    disabled={!wordsForm.id || isSubmitting}
                                />}
                                <Button 
                                    floated='right'
                                    content={id ? 'Done' : 'Create'}
                                    positive
                                    type='submit'
                                    disabled={isSubmitting || !dirty || !isValid}
                                    loading={isSubmitting}
                                />
                            </Segment>
                            <ValidatableTextInput name='name' label='Name' size='large' />
                            <SetFormEditList words={values.words} isSubmitting={isSubmitting} setFieldValue={setFieldValue} />
                        </Form>
                    )}
                </Formik>
            }
        </Segment>
    )
})