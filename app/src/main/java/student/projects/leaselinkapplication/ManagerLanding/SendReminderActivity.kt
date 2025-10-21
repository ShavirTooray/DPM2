package student.projects.leaselinkapplication.ManagerLanding

import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import student.projects.leaselinkapplication.R

class SendReminderActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_send_reminder)

        val sendToInput = findViewById<EditText>(R.id.propertyNameEdit2)
        val reminderTypeInput = findViewById<EditText>(R.id.propertyNameEdit5)
        val messageInput = findViewById<EditText>(R.id.propertyNameEdit6)
        val sendButton = findViewById<Button>(R.id.uploadButton5)

        sendButton.setOnClickListener {
            val sendTo = sendToInput.text.toString().trim()
            val reminderType = reminderTypeInput.text.toString().trim()
            val message = messageInput.text.toString().trim()

            if (sendTo.isEmpty() || reminderType.isEmpty() || message.isEmpty()) {
                Toast.makeText(this, "Please complete all fields", Toast.LENGTH_SHORT).show()
            } else {
                Toast.makeText(
                    this,
                    "Reminder Sent:\nTo: $sendTo\nType: $reminderType\nMessage: $message",
                    Toast.LENGTH_LONG
                ).show()
            }
        }
    }
}