using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartMainMenu : MonoBehaviour
{
    // เรียกเมื่อกดปุ่ม "Play"
    public void StartPlayGame()
    {
        // โหลด Scene ที่ชื่อ "GameScene"
        SceneManager.LoadScene("Level_Tiw");
    }

    // เรียกเมื่อกดปุ่ม "Exit"
    public void StartQuitGame()
    {
        Debug.Log("Quit Game"); // ใช้ทดสอบตอนอยู่ใน Editor
        Application.Quit();     // ใช้งานจริงตอน Build แล้ว
    }
}
