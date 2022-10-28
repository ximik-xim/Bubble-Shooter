using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(Eb<,>))]
public class CustomEdit : PropertyDrawer
{
    
    ReorderableList _listKeyAndData;
    private bool _initialization = false;
    private List<ListErrorId> _listError = new List<ListErrorId>();

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)//сдесь прощитываем высоту компонета
    {
        float maxHeightWindow = 0;

        if (_listKeyAndData != null)
        {
            maxHeightWindow = _listKeyAndData.GetHeight();
        }
        
        return maxHeightWindow;
    }
    
    private void Initialization(Rect position, SerializedProperty property, GUIContent label)
    {
        if (_initialization == false)
        {
            var listKeyAndData = property.FindPropertyRelative("_listKeyAndData");
            _listKeyAndData = new ReorderableList(property.serializedObject,listKeyAndData,true,false,true,true);
            
            _listKeyAndData.drawElementCallback += NewVisualList;
            _listKeyAndData.elementHeightCallback += CheckHeightElement;
            
            _initialization = true;    
        }
    }
    
   public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
   {
       //Rect - прямоугольник в котором будут отображаться поля
      //SerializedProperty - хранит всю инфу о сериализованном поле(название, значение и т.д)
      //GUIContent - метки(метка может хранить в себе текст, изображение и подскаску перед полем)

      Initialization(position, property, label);
      
      EditorGUIUtility.labelWidth = 40;//это устанавливает ширену метки(без этого все идет по пизде, если отображать самописные классы)
      _listKeyAndData.DoList(position);


      FindIdenticalKey();
   }


   private void FindIdenticalKey()
   {
       if (_listKeyAndData.count > 0)
       {
           SerializedPropertyType type = _listKeyAndData.serializedProperty.GetArrayElementAtIndex(0).FindPropertyRelative("_k").propertyType;
           List<int> listExceptions = FillListId();

           if (type != SerializedPropertyType.Generic)
           {
               FindIdenticalElement(listExceptions);
           }
       }
   }
   
   private List<int> FillListId()
   {
       List<int> listIdElement = new List<int>();
       
       for (int i = 0; i < _listKeyAndData.count; i++)
       {
           listIdElement.Add(i);
       }

       return listIdElement;
   }

   
   private void FindIdenticalElement(List<int> listExceptions)
   {
       int j = 0;
       string TextError = "АУ БЛЯТЬ с таким ключем уже есть элементы под номерами ";
       _listError.Clear();

       while (listExceptions.Count > 0) 
       {
           var elementA = _listKeyAndData.serializedProperty.GetArrayElementAtIndex(listExceptions[0]).FindPropertyRelative("_k");
           _listError.Add(new ListErrorId(TextError + listExceptions[0], listExceptions[0]));

           for (int i = 1; i < listExceptions.Count; i++)
           {
               var elementB = _listKeyAndData.serializedProperty.GetArrayElementAtIndex(listExceptions[i]).FindPropertyRelative("_k");
                      
               if (SerializedProperty.DataEquals(elementA, elementB))
               {
                   _listError[j].Id.Add(listExceptions[i]);
                   _listError[j].ErrorText += ", " + listExceptions[i];
                          
                   listExceptions.RemoveAt(i);
                   i--;
               }
           }
                  
           listExceptions.RemoveAt(0);
           j++;
       }
   }
   
   private void NewVisualList(Rect rect, int index, bool isactive, bool isfocused)
   {
       var element = _listKeyAndData.serializedProperty.GetArrayElementAtIndex(index);
       string textEror = CheckeError(index);
       
       rect.y += 15;
       var idElementPosition = new Rect(rect.x, rect.y, 25, EditorGUIUtility.singleLineHeight);
       rect.x += idElementPosition.width;
       var errorPosition = new Rect(rect.x, rect.y, 20, EditorGUIUtility.singleLineHeight);
       rect.x += errorPosition.width + 5;
       var positionField1 = new Rect(rect.x, rect.y, rect.width / 2 - 50, EditorGUIUtility.singleLineHeight);
       rect.x += positionField1.width + 20;
       var positionField2 = new Rect(rect.x, rect.y, rect.width / 2 - 20, EditorGUIUtility.singleLineHeight);       
       
       
       EditorGUI.LabelField(idElementPosition,index.ToString());
       
       if (textEror != null)
       {
           GUIContent errorInfo = new GUIContent("   ",textEror);   
           EditorGUI.HelpBox(errorPosition,"",MessageType.Error);
           EditorGUI.LabelField(errorPosition, errorInfo);
       }
       
       EditorGUI.PropertyField(positionField1, element.FindPropertyRelative("_k"), GUIContent.none,true);
       EditorGUI.PropertyField(positionField2, element.FindPropertyRelative("_d"), GUIContent.none,true);
   }

   private string CheckeError(int index)
   {
       for (int i = 0; i < _listError.Count; i++)
       {
           if (_listError[i].Id.Contains(index) == true && _listError[i].Id.Count > 1)
           {
               return _listError[i].ErrorText;
           }
       }
       
       return null;
   }

  private float CheckHeightElement(int index)//это событие с возращаемым значение(сука а говорили что такую хуйню ни где не юзают, ага блять) и возращаемое значение - это выста элемента списка
  {
      if (_listKeyAndData.count > 0) 
      {
          
          float sizeKey = EditorGUI.GetPropertyHeight(_listKeyAndData.serializedProperty.GetArrayElementAtIndex(index).FindPropertyRelative("_k"), true);
          float sizeData = EditorGUI.GetPropertyHeight(_listKeyAndData.serializedProperty.GetArrayElementAtIndex(index).FindPropertyRelative("_d"), true);
          float maxHeightElement = 0;

          if (sizeData > sizeKey)
          {
              maxHeightElement = sizeData;
          }
          else
          {
              maxHeightElement = sizeKey;
          }

          return maxHeightElement + 20;
      }

      return 0;
  }
  
}

public class ListErrorId
{
    public ListErrorId()
    {
    }

    public ListErrorId(string errorText = "", params int[] iet)
    {
        for (int i = 0; i < iet.Length; i++)
        {
            Id.Add(iet[i]);
        }

        ErrorText = errorText;
    }
    public List<int> Id = new List<int>();
    public string ErrorText;
}